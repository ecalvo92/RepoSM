using System.ComponentModel.DataAnnotations;

namespace SM_API.Models
{
    public class AtenderSolicitudRequestModel
    {
        [Required]
        public int ConsecutivoSolicitud { get; set; }
        [Required]
        public string Solucion { get; set; } = string.Empty;
    }
}
