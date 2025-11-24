using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mercado_libre_frontend.Controllers
{
    [Authorize(Roles = "Usuario,Administrador")]
    public class PedidoController(PedidoClientService pedidos, ProductosClientService productos, IConfiguration configuration) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Pedido>? lista = [];

            try
            {
                lista = await pedidos.GetPedidosAsync();

                if (lista != null)
                {
                    foreach (var pedido in lista)
                    {
                        if (pedido.Items != null)
                        {
                            foreach (var item in pedido.Items)
                            {
                                try
                                {
                                    item.Producto = await productos.GetAsync(item.ProductoId);
                                }
                                catch
                                {
                                    Console.WriteLine($"Error loading product {item.ProductoId}");
                                }
                            }
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }
            if (User.FindFirstValue(ClaimTypes.Role) == "Administrador")
            {
                ViewBag.SoloAdmin = true;
            }
            ViewBag.Url = configuration["UrlWebAPI"];
            return View(lista);
        }


        public async Task<IActionResult> Detalle(int id)
        {
            Pedido? pedido = null;
            try
            {
                pedido = await pedidos.GetPedidoAsync(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                if (pedido.DireccionId > 0)
                {
                    pedido.Direccion = await pedidos.GetDireccionAsync(pedido.DireccionId);
                }

                if (pedido.Items != null)
                {
                    foreach (var item in pedido.Items)
                    {
                        try
                        {
                                item.Producto = await productos.GetAsync(item.ProductoId);   
                        }
                        catch
                        {
                            Console.WriteLine($"Error loading product {item.ProductoId}");
                        }
                    }
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
            return View(pedido);
        }
    }
}
