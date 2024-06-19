using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Validators
{
    public interface ITransportValidator
    {
        bool IsValid(Transport transport);
    }
}
