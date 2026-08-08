namespace SM_API.Models
{
    public class SolicitudChatResponseModel
    {
        public int Consecutivo { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string NombreInterlocutor { get; set; } = string.Empty;
    }
}
