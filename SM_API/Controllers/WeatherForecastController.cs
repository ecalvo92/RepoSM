using Microsoft.AspNetCore.Mvc;

namespace SM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "ObtenerDatos")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
