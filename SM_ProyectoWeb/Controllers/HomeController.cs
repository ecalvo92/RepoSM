using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;

namespace SM_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public HomeController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        #region Iniciar Sesión

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioModel usuario)
        {
            return View();
        }

        #endregion

        #region Crear Usuarios

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(UsuarioModel usuario)
        {
            using (var context = _factory.CreateClient())
            {
                var urlApi = "https://localhost:7149/api/Home/Registro";
                var resultado = context.PutAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                { 
                
                }
            }

            return View();
        }

        #endregion

        #region Recuperar Acceso

        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(UsuarioModel usuario)
        {
            return View();
        }

        #endregion

        public IActionResult Principal()
        {
            return View();
        }

    }
}
