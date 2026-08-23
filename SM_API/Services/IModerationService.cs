namespace SM_API.Services
{
    public interface IModerationService
    {
        // null = service unavailable; true = inappropriate; false = appropriate
        Task<bool?> EsMensajeInapropiadoAsync(string mensaje);
    }
}
