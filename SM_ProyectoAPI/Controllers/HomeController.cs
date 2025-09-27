using Microsoft.AspNetCore.Mvc;

namespace SM_ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult IniciarSesion()
        {
            return Ok();
        }

    }
}
