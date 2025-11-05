using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Mercado_libre_frontend.Controllers
{

    [Authorize(Roles = "Administrador")]
    public class BitacoraController(BitacoraClientService bitacora) : Controller
    {

        public async Task<IActionResult> IndexAsync()
        {
            List<Bitacora>? lista = [];
            try
            {
                lista = await bitacora.GetAsync();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }
            return View(lista);
        }

    }
}
