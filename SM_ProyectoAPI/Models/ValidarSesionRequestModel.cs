using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class ValidarSesionRequestModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
    }
}
