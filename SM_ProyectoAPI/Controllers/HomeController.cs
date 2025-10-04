using Microsoft.AspNetCore.Mvc;
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
            return Ok();
        }

    }
}
