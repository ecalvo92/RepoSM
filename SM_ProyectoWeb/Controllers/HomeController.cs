using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;

namespace SM_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
        public HomeController(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
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
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Home/IniciarSesion";
                var resultado = context.PostAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<UsuarioModel>().Result;

                    if (datosApi != null)
                    {
                        HttpContext.Session.SetString("NombreUsuario", datosApi.Nombre);
                        HttpContext.Session.SetString("NombrePerfil", datosApi.NombrePerfil);

                        return RedirectToAction("Principal", "Home");
                    }
                }

                ViewBag.Mensaje = "No se ha validado la información";
                return View();
            }
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
                var urlApi = _configuration["Valores:UrlAPI"] + "Home/Registro";
                var resultado = context.PostAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                        return RedirectToAction("Index","Home");
                }

                ViewBag.Mensaje = "No se ha registrado la información";
                return View();
            }
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
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Home/RecuperarAcceso?CorreoElectronico=" + usuario.CorreoElectronico;
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<UsuarioModel>().Result;

                    if (datosApi != null)
                        return RedirectToAction("Index", "Home");
                }

                ViewBag.Mensaje = "No se ha recuperado el acceso";
                return View();
            }
        }

        #endregion

        public IActionResult Principal()
        {
            return View();
        }

    }
}
