using SpaceshipCargoTransport.Application.DTOs.Transport;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ITransportService
    {
        Task<TransportReadDTO?> GetDetailsAsync(Guid id);
        Task<TransportReadDTO?> RegisterNewAsync(TransportCreateDTO transport);
        Task<bool> SetToCargoLoadingAsync(Guid id);
        Task<bool> SetToInFlightAsync(Guid id);
        Task<bool> SetToCargoUnloadingAsync(Guid id);
        Task<bool> SetToFinishedAsync(Guid id);
        Task<bool> SetToLostAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }
}
