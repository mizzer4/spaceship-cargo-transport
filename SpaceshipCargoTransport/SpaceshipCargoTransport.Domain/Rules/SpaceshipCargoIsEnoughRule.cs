using SpaceshipCargoTransport.BuildingBlocks;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Rules
{
    public class SpaceshipCargoIsEnoughRule : IBusinessRule
    {
        private readonly Transport _transport;

        public SpaceshipCargoIsEnoughRule(Transport transport)
        {
            _transport = transport;
        }

        public bool IsBroken()
        {
            return _transport.Spaceship.CargoStorageSpace >= _transport.CargoSize;
        }
    }
}
