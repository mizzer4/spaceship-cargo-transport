using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ITransportService
    {
        Task<Transport?> GetDetailsAsync(Guid id);
        Task<bool> RegisterNewAsync(Transport transport);
        Task<bool> SetToCargoLoadingAsync(Guid id);
        Task<bool> SetToInFlightAsync(Guid id);
        Task<bool> SetToCargoUnloadingAsync(Guid id);
        Task<bool> SetToFinishedAsync(Guid id);
        Task<bool> SetToLostAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }
}
