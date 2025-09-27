using Microsoft.AspNetCore.Mvc;

namespace SM_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        public IActionResult Principal()
        {
            return View();
        }

    }
}
