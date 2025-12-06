using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class RegistroCalificacionRequestModel
    {
        [Required]
        public int ConsecutivoProducto { get; set; }
        [Required]
        public int CantidadEstrellas { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
