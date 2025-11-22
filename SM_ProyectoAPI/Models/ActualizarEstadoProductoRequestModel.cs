using System.ComponentModel.DataAnnotations;

namespace SM_ProyectoAPI.Models
{
    public class ActualizarEstadoProductoRequestModel
    {
        [Required]
        public int ConsecutivoProducto { get; set; }
    }
}
