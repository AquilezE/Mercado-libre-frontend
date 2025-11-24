using System.Security.Claims;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mercado_libre_frontend.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class DireccionController(PedidoClientService direcciones, IConfiguration configuration) : Controller
    {
        public IActionResult Crear(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new Direccion());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Direccion direccion, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(direccion.Calle))
            {
                ModelState.AddModelError(nameof(direccion.Calle), "La calle es obligatoria.");
            }
            else if (direccion.Calle.Length > 200)
            {
                ModelState.AddModelError(nameof(direccion.Calle), "La calle no puede exceder 200 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(direccion.Ciudad))
            {
                ModelState.AddModelError(nameof(direccion.Ciudad), "La ciudad es obligatoria.");
            }
            else if (direccion.Ciudad.Length > 100)
            {
                ModelState.AddModelError(nameof(direccion.Ciudad), "La ciudad no puede exceder 100 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(direccion.CodigoPostal))
            {
                ModelState.AddModelError(nameof(direccion.CodigoPostal), "El código postal es obligatorio.");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(direccion.CodigoPostal, @"^\d{5}$"))
            {
                ModelState.AddModelError(nameof(direccion.CodigoPostal), "El código postal debe tener exactamente 5 dígitos.");
            }

            if (ModelState.IsValid)
            {
                direccion.Calle = System.Net.WebUtility.HtmlEncode(direccion.Calle.Trim());
                direccion.Ciudad = System.Net.WebUtility.HtmlEncode(direccion.Ciudad.Trim());
                direccion.CodigoPostal = direccion.CodigoPostal.Trim();

                try
                {
                    await direcciones.CrearDireccionAsync(direccion);
                    TempData["SuccessMessage"] = "Dirección agregada exitosamente.";

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Checkout", "Carrito");
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Salir", "Auth");
                    }

                    ModelState.AddModelError("", "Error del servidor. Inténtelo de nuevo.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error inesperado. Inténtelo de nuevo.");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(direccion);
        }
    }
}