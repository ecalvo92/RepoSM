using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;
using System.Net.Http.Headers;
using Utiles;
using static System.Net.Mime.MediaTypeNames;

namespace SM_ProyectoWeb.Controllers
{
    [Seguridad]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
        public UsuarioController(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
        }

        #region Empresa

        [HttpGet]
        public IActionResult Empresa()
        {
            var consecutivoUsuario = HttpContext.Session.GetInt32("ConsecutivoUsuario");

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Usuario/ConsultarUsuario?ConsecutivoUsuario=" + consecutivoUsuario;
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<UsuarioModel>().Result;

                    if (datosApi != null)
                        return View(datosApi);
                }

                ViewBag.Mensaje = "No se ha recuperado correctamente su información";
                return View(new UsuarioModel());
            }
        }

        [HttpPost]
        public IActionResult Empresa(UsuarioModel usuario, IFormFile ImagenComercial)
        {
            usuario.ImagenComercial = "/empresas/";

            ViewBag.Mensaje = "La información no se ha actualizado correctamente";
            usuario.ConsecutivoUsuario = (int)HttpContext.Session.GetInt32("ConsecutivoUsuario")!;

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Usuario/ActualizarEmpresa";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PutAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                    {
                        GuardarDatosImagen(ImagenComercial, usuario.ConsecutivoUsuario);
                        ViewBag.Mensaje = "La información se ha actualizado correctamente";
                    }
                }

                return View(usuario);
            }
        }

        #endregion

        #region Perfil

        [HttpGet]
        public IActionResult InfoPerfil()
        {
            var consecutivoUsuario = HttpContext.Session.GetInt32("ConsecutivoUsuario");

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Usuario/ConsultarUsuario?ConsecutivoUsuario=" + consecutivoUsuario;
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<UsuarioModel>().Result;

                    if (datosApi != null)
                        return View(datosApi);
                }

                ViewBag.Mensaje = "No se ha recuperado correctamente su información";
                return View(new UsuarioModel());
            }
        }

        [HttpPost]
        public IActionResult InfoPerfil(UsuarioModel usuario)
        {
            ViewBag.Mensaje = "La información no se ha actualizado correctamente";
            usuario.ConsecutivoUsuario = (int)HttpContext.Session.GetInt32("ConsecutivoUsuario")!;

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Usuario/ActualizarPerfil";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PutAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                    {
                        HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                        ViewBag.Mensaje = "La información se ha actualizado correctamente";
                    }
                }

                return View();
            }
        }

        #endregion

        #region Seguridad

        [HttpGet]
        public IActionResult InfoSeguridad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InfoSeguridad(UsuarioModel usuario)
        {
            Helper h = new Helper();
            usuario.Contrasenna = h.Encrypt(usuario.Contrasenna);

            ViewBag.Mensaje = "La información no se ha actualizado correctamente";
            usuario.ConsecutivoUsuario = (int)HttpContext.Session.GetInt32("ConsecutivoUsuario")!;

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Usuario/ActualizarSeguridad";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PutAsJsonAsync(urlApi, usuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                        ViewBag.Mensaje = "La información se ha actualizado correctamente";
                }

                return View();
            }
        }

        #endregion

        private void GuardarDatosImagen(IFormFile Imagen, int Consecutivo)
        {
            if (Imagen != null)
            {
                //save imagen 
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "empresas");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreImagen = Consecutivo + ".png";
                var carpetaFinal = Path.Combine(carpeta, nombreImagen);

                using (var stream = new FileStream(carpetaFinal, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
            }
        }

    }
}
