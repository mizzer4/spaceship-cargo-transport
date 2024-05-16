using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Validators
{
    public class TransportValidator : ITransportValidator
    {
        public bool IsValid(Transport transport)
        {
            // TODO
            var isValid = IsSpaceshipCargoSpaceEnough(transport) &&
                IsDestinationDifferentPlanet(transport);

            return isValid;
        }

        public bool IsSpaceshipCargoSpaceEnough(Transport transport)
        {
            return transport.Spaceship.CargoStorageSpace >= transport.CargoSize;
        }

        public bool IsDestinationDifferentPlanet(Transport transport)
        {
            return transport.StartingLocation != transport.EndingLocation;
        }
    }
}
