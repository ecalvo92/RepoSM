using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class EmpresaRequestModel
    {
        [Required]
        public int ConsecutivoUsuario { get; set; }
        [Required]
        public string NombreComercial { get; set; } = string.Empty;
        [Required]
        public string ImagenComercial { get; set; } = string.Empty;
    }
}
