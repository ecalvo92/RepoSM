using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_ProyectoAPI.Models;

namespace SM_ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPut]
        [Route("Registro")]
        public IActionResult Registro(UsuarioModel usuario)
        {
            using (var context = new SqlConnection("Server=localhost; Database=BD_SM; Integrated Security=True; TrustServerCertificate=True;"))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Identificacion", usuario.Identificacion);
                parametros.Add("@Nombre", usuario.Nombre);
                parametros.Add("@CorreoElectronico", usuario.CorreoElectronico);
                parametros.Add("@Contrasenna", usuario.Contrasenna);

                var resultado = context.Execute("Registro", parametros);
                return Ok(resultado);
            }
        }

    }
}
