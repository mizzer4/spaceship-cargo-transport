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
                return await _transportRepository.CreateAsync(transport);
            }         

            return false;
        }

        public async Task<bool> SetToCargoLoadingAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.CargoLoading);

            return isSuccessful;
        }

        public async Task<bool> SetToInFlightAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.InFlight);

            return isSuccessful;
        }

        public async Task<bool> SetToCargoUnloadingAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.CargoUnloading);

            return isSuccessful;
        }

        public async Task<bool> SetToFinishedAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.Finished);

            if (isSuccessful)
            {
                _notificationService.NotifyFinished(transport);
                return true;
            }

            return false;
        }

        public async Task<bool> SetToLostAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.Lost);

            if (isSuccessful)
            {
                _notificationService.NotifyLost(transport);
                return true;
            }

            return false;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            var isSuccessful = await SetStatus(transport, TransportStatus.Cancelled);

            if (isSuccessful)
            {
                _notificationService.NotifyCancelled(transport);
                return true;
            }

            return false;
        }

        private async Task<bool> SetStatus(Transport transport, TransportStatus transportStatus)
        {
            if (transport != null)
            {
                transport.Status = transportStatus;
                return await _transportRepository.UpdateAsync(transport);
            }

            return false;
        }
    }
}
