using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Validators
{
    internal class TransportValidator : ITransportValidator
    {
        public bool IsValid(Transport transport)
        {
            // TODO
            var isValid = IsSpaceshipCargoSpaceEnough(transport) &&
                IsDestinationDifferentPlanet(transport);

            return true;
        }

        private bool IsSpaceshipCargoSpaceEnough(Transport transport)
        {
            return transport.Spaceship.CargoStorageSize >= transport.CargoSize;
        }

        private bool IsDestinationDifferentPlanet(Transport transport)
        {
            return transport.StartingLocation != transport.EndingLocation;
        }
    }
}
