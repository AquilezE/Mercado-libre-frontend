using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Mercado_libre_frontend.Controllers
{

    [Authorize(Roles = "Usuario")]
    public class ComprarController(ProductosClientService productos, IConfiguration configuration): Controller
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


    }
}
