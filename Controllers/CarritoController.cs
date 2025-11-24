using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Mercado_libre_frontend.Controllers
{

    [Authorize(Roles = "Usuario")]
    public class CarritoController(CarritoClientService carrito, ProductosClientService productos, PedidoClientService pedidos, IConfiguration configuration) : Controller
    {


        public async Task<IActionResult> Index()
        {
            Carrito? carritoData = new();

            try
            {
                carritoData = await carrito.GetAsync();

                if (carritoData?.Items != null)
                {

                    foreach (var item in carritoData.Items)
                    {




                        try
                        {
                            if (int.TryParse(item.ProductoId.ToString(), out int prodId))
                            {
                                item.Producto = await productos.GetAsync(prodId);
                            }
                        }
                        catch
                        {
                            // Ignore individual product fetch errors
                        }

                    }
                }
                else
                {
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }

            ViewBag.Url = configuration["UrlWebAPI"];
            return View(carritoData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarCantidad(int itemId, int cantidad)
        {
            if (cantidad <= 0 || cantidad > 10)
            {
                TempData["ErrorMessage"] = "Cantidad inválida. La cantidad debe estar entre 1 y 10.";
                return RedirectToAction("Index");
            }
            try
            {
                await carrito.PatchAsync(itemId, cantidad);
                TempData["SuccessMessage"] = "Cantidad actualizada correctamente.";
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                TempData["ErrorMessage"] = "Error al actualizar la cantidad.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Error al actualizar la cantidad.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverItem(int itemId)
        {


            if (itemId <= 0)
            {
                TempData["ErrorMessage"] = "ID de item inválido.";
                return RedirectToAction("Index");
            }

            try
            {
                await carrito.DeleteAsync(itemId);
                TempData["SuccessMessage"] = "Item removido correctamente.";
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }


                TempData["ErrorMessage"] = "Error al remover el item.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Error al remover el item.";
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoverConfirmar(int itemId, bool? showError = false)
        {



            Carrito? carritoData = null;
            CarritoItem itemToRemove = null;


            try
            {
                carritoData = await carrito.GetAsync();


                if (carritoData?.Items != null)
                {
                    foreach (var item in carritoData.Items)
                    {
                    }
                }

                itemToRemove = carritoData?.Items?.FirstOrDefault(i => i.ItemId == itemId);


                if (itemToRemove == null)
                {

                    return NotFound();
                }

                itemToRemove.Producto = await productos.GetAsync((int)itemToRemove.ProductoId);

                if (showError.GetValueOrDefault())
                {
                    ViewData["ErrorMessage"] = "Error al remover el item. Por favor, intente de nuevo.";
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }

            }

            ViewBag.Url = configuration["UrlWebAPI"];
            return View(itemToRemove);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LimpiarCarrito()
        {
            try
            {
                await carrito.ClearAsync();
                TempData["SuccessMessage"] = "Carrito limpiado correctamente.";
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                TempData["ErrorMessage"] = "Error al limpiar el carrito.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Error al limpiar el carrito.";
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Checkout()
        {
            Carrito? carritoData = new();
            List<Direccion> direcciones = [];

            try
            {
                carritoData = await carrito.GetAsync();
                direcciones = await pedidos.GetDireccionesAsync();




                if (carritoData?.Items == null || carritoData.Items.Count == 0)
                {
                    TempData["ErrorMessage"] = "Su carrito está vacío. Agregue productos antes de proceder al checkout.";
                    return RedirectToAction("Index");
                }



                foreach (var item in carritoData.Items)
                {
                    try
                    {
                        item.Producto = await productos.GetAsync(item.ProductoId);
                    }
                    catch { }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                TempData["ErrorMessage"] = "Error al obtener datos del checkout.";
                return RedirectToAction("Index");
            }

            ViewBag.Url = configuration["UrlWebAPI"];
            ViewBag.Carrito = carritoData;
            ViewBag.Direcciones = direcciones;
            return View(new CheckoutRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            if (request.DireccionId <= 0)
            {
                TempData["ErrorMessage"] = "Debe seleccionar una dirección de entrega.";
                return RedirectToAction("Checkout");
            }

            try
            {
                await pedidos.CrearPedidoAsync(request.DireccionId);
                return RedirectToAction("CheckoutSuccess");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                TempData["ErrorMessage"] = "Error del servidor al procesar el pedido.";
                return RedirectToAction("Checkout");
            }
            catch
            {
                TempData["ErrorMessage"] = "Error inesperado al procesar el pedido.";
                return RedirectToAction("Checkout");
            }
        }


        public async Task<IActionResult> CheckoutSuccess()
        {
            ViewData["Title"] = "Pedido Realizado";
            ViewData["Message"] = "¡Su pedido ha sido realizado con éxito! Gracias por comprar con nosotros.";
            return View();
        }
    }
}
