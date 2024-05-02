using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Validators
{
    internal interface ITransportValidator
    {
        bool IsValid(Transport transport);
    }
}
