using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface IPlanetService
    {
        Planet Get(int id);
        IEnumerable<Planet> GetAll();
        bool Create(Planet planet);
        bool Update(Planet planet);
        bool Delete(int id);
    }
}
