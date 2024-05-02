using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Domain.Validators;

namespace SpaceshipCargoTransport.Domain.Services
{
    internal class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly ITransportValidator _transportValidator;

        public TransportService(ITransportRepository transportRepository, ITransportValidator transportValidator)
        {                                      
            _transportRepository = transportRepository;
            _transportValidator = transportValidator;
        }

        public bool Cancel(int id)
        {
            return _transportRepository.Cancel(id);
        }

        public Transport GetDetails(int id)
        {
            return _transportRepository.GetDetails(id);
        }

        public bool MarkAsFinished(Transport transport)
        {
            return _transportRepository.MarkAsFinished(transport);
        }

        public bool RegisterNew(Transport transport)
        {
            if (!_transportValidator.IsValid(transport))
                return false;

            return _transportRepository.RegisterNew(transport);
        }
    }
}
