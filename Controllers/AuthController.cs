using System.Security.Claims;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Mercado_libre_frontend.Controllers
{
    public class AuthController(AuthClientService auth) : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await auth.ObtenTokenAsync(model.Email, model.Password);
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name,token.Email),
                        new(ClaimTypes.GivenName,token.Nombre),
                        new("jwt",token.Jwt),
                        new(ClaimTypes.Role, token.Rol)
                    };
                    await auth.IniciaSesionAsync(claims);
                    //Usuario chido
                    if (token.Rol == "Administrador")
                    {
                        return RedirectToAction("Index", "Productos");
                    }
                    else
                    {
                        Console.WriteLine("Usuario normal");
                        Console.WriteLine(token.Jwt);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Email", "Credenciales no validas. Intentelo nuevamente");
                }
            }

            return View(model);
        }


        [Authorize(Roles = "Administrador,Usuario")]
        public async Task<IActionResult> SalirAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Auth");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Crear(int? exito =null)
        {
	    ViewBag.Exito = exito;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAsync(UsuarioPwd model)
        {
            if (ModelState.IsValid)
            {
                try
                {

		    Console.WriteLine("=========");	
		    Console.WriteLine(model);	
		    Console.WriteLine("---------");
		    await auth.RegistrarUsuarioAsync(model);
		    return RedirectToAction("Crear", "Auth", new { exito = 1 });
                }                catch (Exception ex)
		                {
					                    ModelState.AddModelError("Email", "No ha sido posible crear la cuenta con este correo.");
							                    }
            }

            return View(model);
        }
    }
}
