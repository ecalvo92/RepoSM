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

        [HttpGet]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                var resultado = context.Query<ValidarSesionResponse>("ConsultarUsuarios", parametros);

                if (resultado.Any())
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


        [HttpPut]
        [Route("ActualizarEmpresa")]
        public IActionResult ActualizarEmpresa(EmpresaRequestModel empresa)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoUsuario", empresa.ConsecutivoUsuario);
                parametros.Add("@NombreComercial", empresa.NombreComercial);
                parametros.Add("@ImagenComercial", empresa.ImagenComercial);

                var resultado = context.Execute("ActualizarEmpresa", parametros);
                return Ok(resultado);
            }
        }
        

        [HttpPut]
        [Route("ActualizarSeguridad")]
        public IActionResult ActualizarSeguridad(SeguridadRequestModel usuario)
        {
            using (var context = new SqlConnection(_configuration["ConnectionStrings:BDConnection"]))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ConsecutivoUsuario", usuario.ConsecutivoUsuario);
                parametros.Add("@Contrasenna", usuario.Contrasenna);

                var resultado = context.Execute("ActualizarContrasenna", parametros);
                return Ok(resultado);
            }
        }

    }
}
