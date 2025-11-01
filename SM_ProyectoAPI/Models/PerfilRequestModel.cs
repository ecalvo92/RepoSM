using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class PerfilRequestModel
    {
        [Required]
        public int ConsecutivoUsuario { get; set; }
        [Required]
        public string Identificacion { get; set; } = string.Empty;
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
