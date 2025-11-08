using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;
using System.Net.Http.Headers;

namespace SM_ProyectoWeb.Controllers
{
    [Seguridad]
    public class ProductoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
        public ProductoController(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Producto/ConsultarProductos";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<List<ProductoModel>>().Result;

                    return View(datosApi);
                }

                ViewBag.Mensaje = "No hay productos registrados en este momento";
                return View(new List<ProductoModel>());
            }
        }

        [HttpGet]
        public IActionResult AgregarProductos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarProductos(ProductoModel producto, IFormFile Imagen)
        {
            producto.Imagen = "/imagenes/";

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Producto/RegistroProductos";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PostAsJsonAsync(urlApi, producto).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                    {
                        //save imagen 
                        var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

                        if (!Directory.Exists(carpeta))
                            Directory.CreateDirectory(carpeta);

                        var nombreImagen = datosApi + ".png";
                        var carpetaFinal = Path.Combine(carpeta, nombreImagen);

                        using (var stream = new FileStream(carpetaFinal, FileMode.Create))
                        {
                            Imagen.CopyTo(stream);
                        }

                        return RedirectToAction("ConsultarProductos", "Producto");
                    }
                }

                ViewBag.Mensaje = "No se ha registrado la información";
                return View();
            }
        }


        [HttpGet]
        public IActionResult ActualizarProductos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ActualizarProductos(ProductoModel producto, IFormFile Imagen)
        {
            return View();
        }

    }
}
