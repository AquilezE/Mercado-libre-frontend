using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Mercado_libre_frontend.Controllers
{

    [Authorize(Roles = "Usuario")]
    public class ComprarController(ProductosClientService productos, CarritoClientService carrito, IConfiguration configuration): Controller
    {

        public async Task<IActionResult> Index(string? s)
        {
            List<Producto>? lista = [];
            try
            {
                lista = await productos.GetAsync(s);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }

            ViewBag.Url = configuration["UrlWebAPI"];
            ViewBag.search= s;
            return View(lista);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            Producto? producto = null;
            try
            {
                producto = await productos.GetAsync(id);
                if (producto == null)
                {
                    return NotFound();
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
            return View(producto);
        }

        // Add to cart - standard form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarAlCarrito(int productoId, int cantidad)
        {
            if (!ModelState.IsValid || productoId <= 0 || cantidad <= 0 || cantidad > 10)
            {
                TempData["ErrorMessage"] = "Datos inválidos. La cantidad debe estar entre 1 y 10.";
                return RedirectToAction("Detalle", new { id = productoId });
            }

            try
            {
                
                await carrito.PostAsync(productoId, cantidad);

                return RedirectToAction("Index", "Carrito");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                TempData["ErrorMessage"] = "Error del servidor. Inténtelo de nuevo.";
                return RedirectToAction("Detalle", new { id = productoId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error al agregar al carrito. Inténtelo de nuevo.";
                return RedirectToAction("Detalle", new { id = productoId });
            }
        }




    }
}
