using System.Security.Claims;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Mercado_libre_frontend.Controllers
{
    [Authorize]
    public class PerfilController(PerfilClientService perfil) : Controller
    {

        public async Task<IActionResult> IndexAsync()
        {
            AuthUser? usuario = null;
            try
            {

                ViewBag.timeRemaining = await perfil.ObtenTiempoAsync();

                usuario = new AuthUser
                {
                    Email = User.FindFirstValue(ClaimTypes.Name)!,
                    Nombre = User.FindFirstValue(ClaimTypes.GivenName)!,
                    Rol = User.FindFirstValue(ClaimTypes.Role)!,
                    Jwt = User.FindFirstValue("jwt")!
                };
               
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }
            return View(usuario);
        }
    }
}
