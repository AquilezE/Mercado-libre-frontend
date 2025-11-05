using System.Security.Claims;
using Mercado_libre_frontend.Models;
using Mercado_libre_frontend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mercado_libre_frontend.Controllers
{

    [Authorize(Roles = "Usuario")]
    public class CarritoController() : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
