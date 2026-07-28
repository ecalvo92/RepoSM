using System.ComponentModel.DataAnnotations;

namespace SM_API.Models
{
    public class CambiarAccesoRequestModel
    {
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
    }
}
