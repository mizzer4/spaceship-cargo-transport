using SpaceshipCargoTransport.BuildingBlocks;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Rules
{
    public class TransportDestinationIsDifferentPlanetRule : IBusinessRule
    {
        private readonly Transport _transport;

        public TransportDestinationIsDifferentPlanetRule(Transport transport)
        {
            _transport = transport;
        }

        public bool IsBroken()
        {
            return _transport.StartingLocation != _transport.EndingLocation;
        }
    }
}
