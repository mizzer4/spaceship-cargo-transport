using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Repositories
{
    public interface IPlanetRepository
    {
        Planet Get(int id);
        IEnumerable<Planet> GetAll();
        bool Create(Planet planet);
        bool Update(Planet planet);
        bool Delete(int id);
    }
}
