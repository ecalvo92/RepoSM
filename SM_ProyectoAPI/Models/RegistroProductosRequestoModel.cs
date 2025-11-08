using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class RegistroProductosRequestoModel
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public string Imagen { get; set; } = string.Empty;
    }
}
