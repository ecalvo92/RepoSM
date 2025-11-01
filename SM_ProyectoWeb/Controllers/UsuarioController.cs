using Microsoft.AspNetCore.Mvc;
using SM_ProyectoWeb.Models;

namespace SM_ProyectoWeb.Controllers
{
    [Seguridad]
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult InfoPerfil()
        {
            return View();
        }
    }
}
