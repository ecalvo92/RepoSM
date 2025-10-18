using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace SM_ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ErrorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("RegistrarError")]
        public IActionResult RegistrarError()
        {
            var excepcion = HttpContext.Features.Get<IExceptionHandlerFeature>();

            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoUsuario", 0);
                parametros.Add("@Mensaje", excepcion?.Error.Message);
                parametros.Add("@Origen", excepcion?.Path);

                var resultado = context.Execute("RegistrarError", parametros);
            }

            return StatusCode(500, "Se presentó una excepción en nuestro servicio");
        }

    }
}
