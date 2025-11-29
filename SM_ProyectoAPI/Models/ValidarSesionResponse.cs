namespace SM_ProyectoAPI.Models
{
    public class ValidarSesionResponse
    {
        public int ConsecutivoUsuario { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int ConsecutivoPerfil { get; set; }
        public string NombrePerfil { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public string NombreComercial { get; set; } = string.Empty;
        public string ImagenComercial { get; set; } = string.Empty;
    }
}
