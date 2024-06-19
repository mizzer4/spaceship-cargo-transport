using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.Transport;
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
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository transportRepository, ITransportValidator transportValidator, ITransportNotificationService notificationService, IMapper mapper)
        {                                      
            _transportRepository = transportRepository;
            _transportValidator = transportValidator;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<TransportReadDTO?> GetDetailsAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);
            return _mapper.Map<TransportReadDTO>(transport);
        }

        public async Task<TransportReadDTO?> RegisterNewAsync(TransportCreateDTO transportDTO)
        {
            var transport = _mapper.Map<Transport>(transportDTO);
            transport.Id = Guid.NewGuid();

            if (_transportValidator.IsValid(transport) && 
                await _transportRepository.CreateAsync(transport))
            {
                return _mapper.Map<TransportReadDTO>(transport);
            }         

            return null;
        }

        public async Task<bool> SetToCargoLoadingAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);

            return await SetStatus(transport, TransportStatus.CargoLoading);
        }

        public async Task<bool> SetToInFlightAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);

            return await SetStatus(transport, TransportStatus.InFlight);
        }

        public async Task<bool> SetToCargoUnloadingAsync(Guid id)
        {
            var transport = await _transportRepository.GetAsync(id);

            return await SetStatus(transport, TransportStatus.CargoUnloading);
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

        private async Task<bool> SetStatus(Transport? transport, TransportStatus transportStatus)
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
