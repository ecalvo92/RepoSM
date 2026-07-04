using System.ComponentModel.DataAnnotations;

namespace SM_API.Models
{
    public class RecuperarAccesoRequestModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
