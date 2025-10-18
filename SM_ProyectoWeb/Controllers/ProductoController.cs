using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;

namespace SM_ProyectoWeb.Controllers
{
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
    }
}
