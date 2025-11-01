using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_ProyectoAPI.Models;

namespace SM_ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario(int ConsecutivoUsuario)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoUsuario", ConsecutivoUsuario);
                var resultado = context.QueryFirstOrDefault<ValidarSesionResponse>("ConsultarUsuario", parametros);

                if (resultado != null)
                    return Ok(resultado);

                return NotFound();
            }
        }

        [HttpPut]
        [Route("ActualizarPerfil")]
        public IActionResult ActualizarPerfil(PerfilRequestModel usuario)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoUsuario", usuario.ConsecutivoUsuario);
                parametros.Add("@Identificacion", usuario.Identificacion);
                parametros.Add("@Nombre", usuario.Nombre);
                parametros.Add("@CorreoElectronico", usuario.CorreoElectronico);
                
                var resultado = context.Execute("ActualizarPerfil", parametros);
                return Ok(resultado);
            }
        }       

    }
}
