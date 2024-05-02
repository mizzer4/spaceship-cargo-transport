using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;

namespace SpaceshipCargoTransport.Domain.Services
{
    internal class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;

        public PlanetService(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        public bool Create(Planet planet)
        {
            return _planetRepository.Create(planet);
        }

        public bool Delete(int id)
        {
            return _planetRepository.Delete(id);
        }

        public Planet Get(int id)
        {
            return _planetRepository.Get(id);
        }

        public IEnumerable<Planet> GetAll()
        {
            return _planetRepository.GetAll();
        }

        public bool Update(Planet planet)
        {
            return _planetRepository.Update(planet);
        }
    }
}
