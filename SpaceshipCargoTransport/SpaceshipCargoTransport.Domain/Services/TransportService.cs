using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Notifications;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Validators;

namespace SpaceshipCargoTransport.Domain.Services
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly ITransportValidator _transportValidator;
        private readonly ITransportNotificationService _notificationService;

        public TransportService(ITransportRepository transportRepository, ITransportValidator transportValidator, ITransportNotificationService notificationService)
        {                                      
            _transportRepository = transportRepository;
            _transportValidator = transportValidator;
            _notificationService = notificationService;
        }

        public Task<Transport?> GetDetailsAsync(Guid id)
        {
            return _transportRepository.GetAsync(id);
        }

        public async Task<bool> RegisterNewAsync(Transport transport)
        {
            transport.Id = Guid.NewGuid();

            if (_transportValidator.IsValid(transport))
            {
                await _transportRepository.CreateAsync(transport);
                return true;
            }         

            return false;
        }

        public async Task<bool> SetToCargoLoadingAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.CargoLoading);
            return transport != null;
        }

        public async Task<bool> SetToInFlightAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.InFlight);
            return transport != null;
        }

        public async Task<bool> SetToCargoUnloadingAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.CargoUnloading);
            return transport != null;
        }

        public async Task<bool> SetToFinishedAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.Finished);

            if (transport != null)
            {
                _notificationService.NotifyFinished(transport);
                return true;
            }

            return false;
        }

        public async Task<bool> SetToLostAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.Lost);

            if (transport != null)
            {
                _notificationService.NotifyLost(transport);
                return true;
            }

            return false;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var transport = await GetAndSetStatus(id, TransportStatus.Cancelled);

            if (transport != null)
            {
                _notificationService.NotifyCancelled(transport);
                return true;
            }

            return false;
        }

        private async Task<Transport?> GetAndSetStatus(Guid id, TransportStatus transportStatus)
        {
            var transport = await _transportRepository.GetAsync(id);

            if (transport != null)
            {
                transport.Status = transportStatus;
                await _transportRepository.UpdateAsync(transport);
            }

            return transport;
        }
    }
}
