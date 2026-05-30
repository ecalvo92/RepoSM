using Microsoft.AspNetCore.Mvc;
using SM_WEB.Models;
using System.Diagnostics;

namespace SM_WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Registrar Usuarios

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(UsuarioModel model)
        {
            //Programar

            return View();
        }

        #endregion

        public IActionResult RecuperarAcceso()
        {
            return View();
        }
    }
}
