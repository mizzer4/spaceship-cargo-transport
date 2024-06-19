using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Rules;

namespace SpaceshipCargoTransport.Domain.Validators
{
    public class TransportValidator : ITransportValidator
    {
        public bool IsValid(Transport transport)
        {
            var isValid = 
                new SpaceshipCargoIsEnoughRule(transport).IsBroken() &&
                new TransportDestinationIsDifferentPlanetRule(transport).IsBroken();

            return isValid;
        }
    }
}
