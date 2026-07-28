using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Models;
using SM_API.Services;

namespace SM_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController(IConfiguration _config, IUtilesService _utiles) : ControllerBase
    {

        [HttpPost("RegistrarSolicitudAPI")]
        public IActionResult RegistrarSolicitudAPI(RegistroSolicitudRequestModel model)
        {
            using var context = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);

            var parameters = new DynamicParameters();
            parameters.Add("@Titulo", model.Titulo);
            parameters.Add("@Descripcion", model.Descripcion);
            parameters.Add("@ConsecutivoUsuario", _utiles.ObtenerConsecutivoToken());

            var response = context.Execute("spRegistrarSolicitud", parameters);

            if (response > 0)
            {
                return Ok("La solicitud se ha registrado correctamente");
            }

            return BadRequest("No se ha podido registrar la solicitud");
        }

    }
}
