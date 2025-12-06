using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

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
            //id 0 = todos los productos
            var respuesta = ConsultarDatosProductos(0);
            return View(respuesta);
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
                        GuardarDatosImagen(Imagen, datosApi);
                        return RedirectToAction("ConsultarProductos", "Producto");
                    }
                }

                ViewBag.Mensaje = "No se ha registrado la información";
                return View();
            }
        }


        [HttpGet]
        public IActionResult ActualizarProductos(int id)
        {
            //id = el producto especifico a mostrar
            var respuesta = ConsultarDatosProductos(id);
            return View(respuesta?.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult ActualizarProductos(ProductoModel producto, IFormFile Imagen)
        {
            producto.Imagen = "/imagenes/";

            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Producto/ActualizarProductos";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PutAsJsonAsync(urlApi, producto).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                    {
                        GuardarDatosImagen(Imagen, producto.ConsecutivoProducto);
                        return RedirectToAction("ConsultarProductos", "Producto");
                    }
                }

                ViewBag.Mensaje = "No se ha registrado la información";
                return View();
            }
        }


        [HttpPost]
        public IActionResult CambiarEstadoProducto(ProductoModel producto)
        {
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Producto/CambiarEstadoProducto";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.PutAsJsonAsync(urlApi, producto).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<int>().Result;

                    if (datosApi > 0)
                    {
                        return RedirectToAction("ConsultarProductos", "Producto");
                    }
                }

                ViewBag.Mensaje = "No se ha actualizado el estado del producto";
                return View();
            }
        }


        [HttpGet]
        public IActionResult VerProductosEmpresa(int id)
        {
            var respuesta = ConsultarDatosProductosEmpresa(id, 0);
            return View(respuesta);
        }

        private List<ProductoModel>? ConsultarDatosProductos(int id)
        {
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + "Producto/ConsultarProductos?ConsecutivoProducto=" + id;
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<List<ProductoModel>>().Result;

                    return datosApi;
                }

                ViewBag.Mensaje = "No hay productos registrados en este momento";
                return new List<ProductoModel>();
            }
        }

        private List<ProductoModel>? ConsultarDatosProductosEmpresa(int idUsuario, int idProducto)
        {
            using (var context = _factory.CreateClient())
            {
                var urlApi = _configuration["Valores:UrlAPI"] + $"Producto/ConsultarProductosEmpresa?ConsecutivoProducto={idProducto}&ConsecutivoUsuario={idUsuario}";
                context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var resultado = context.GetAsync(urlApi).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datosApi = resultado.Content.ReadFromJsonAsync<List<ProductoModel>>().Result;

                    return datosApi;
                }

                ViewBag.Mensaje = "No hay productos registrados en este momento";
                return new List<ProductoModel>();
            }
        }

        private void GuardarDatosImagen(IFormFile Imagen, int ConsecutivoProducto)
        {
            if (Imagen != null)
            {
                //save imagen 
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreImagen = ConsecutivoProducto + ".png";
                var carpetaFinal = Path.Combine(carpeta, nombreImagen);

                using (var stream = new FileStream(carpetaFinal, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
            }
        }

    }
}
