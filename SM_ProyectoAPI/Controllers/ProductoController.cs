using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_ProyectoAPI.Models;

namespace SM_ProyectoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos(int ConsecutivoProducto)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoProducto", ConsecutivoProducto);
                parametros.Add("@ConsecutivoUsuario", HttpContext.User.FindFirst("userId")?.Value);

                var resultado = context.Query<ProductoResponse>("ConsultarProductos", parametros);

                return Ok(resultado);
            }
        }


        [HttpPost]
        [Route("RegistroProductos")]
        public IActionResult RegistroProductos(RegistroProductosRequestModel producto)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Nombre", producto.Nombre);
                parametros.Add("@Descripcion", producto.Descripcion);
                parametros.Add("@Precio", producto.Precio);
                parametros.Add("@Imagen", producto.Imagen);
                parametros.Add("@ConsecutivoUsuario", HttpContext.User.FindFirst("userId")?.Value);

                var resultado = context.QueryFirstOrDefault<ProductoResponse>("RegistroProductos", parametros);
                return Ok(resultado!.ConsecutivoProducto);
            }
        }


        [HttpPut]
        [Route("ActualizarProductos")]
        public IActionResult ActualizarProductos(ActualizarProductoRequestModel producto)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoProducto", producto.ConsecutivoProducto);
                parametros.Add("@Nombre", producto.Nombre);
                parametros.Add("@Descripcion", producto.Descripcion);
                parametros.Add("@Precio", producto.Precio);
                parametros.Add("@Imagen", producto.Imagen);
                parametros.Add("@ConsecutivoUsuario", HttpContext.User.FindFirst("userId")?.Value);

                var resultado = context.Execute("ActualizarProductos", parametros);
                return Ok(resultado);
            }
        }


        [HttpPut]
        [Route("CambiarEstadoProducto")]
        public IActionResult CambiarEstadoProducto(ActualizarEstadoProductoRequestModel producto)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoProducto", producto.ConsecutivoProducto);
                parametros.Add("@ConsecutivoUsuario", HttpContext.User.FindFirst("userId")?.Value);

                var resultado = context.Execute("ActualizarEstadoProducto", parametros);
                return Ok(resultado);
            }
        }
        
    }
}
