using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ISpaceshipService
    {
        Spaceship? Get(Guid id);
        IEnumerable<Spaceship> GetAll();
        bool Create(Spaceship ship);
        bool Update(Spaceship ship);
        bool Delete(Spaceship ship);
    }
}
