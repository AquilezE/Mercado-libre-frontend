using Microsoft.AspNetCore.Mvc;

namespace Mercado_libre_frontend.Controllers
{


    public class HomeController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }   

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error([FromServices] IHostEnvironment hostEnvironment)
        {
            return View();
        }

    }
}
