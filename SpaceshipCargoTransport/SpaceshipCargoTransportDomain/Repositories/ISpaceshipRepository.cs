using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Repositories
{
    public interface ISpaceshipRepository
    {
        Spaceship? Get(Guid id);
        IEnumerable<Spaceship> GetAll();
        bool Create(Spaceship ship);
        bool Update(Spaceship ship);
        bool Delete(Spaceship ship);
    }
}
