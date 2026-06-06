using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM_API.Models;

namespace SM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("RegistroAPI")]
        public IActionResult RegistroAPI(UsuarioModel model)
        {
            return Ok();
        }

    }
}
