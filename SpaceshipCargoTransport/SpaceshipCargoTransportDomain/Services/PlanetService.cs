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

        public Task<bool> CreateAsync(Planet planet)
        {
            return _planetRepository.CreateAsync(planet);
        }

        public Task<bool> DeleteAsync(Planet planet)
        {
            return _planetRepository.DeleteAsync(planet);
        }

        public Task<Planet> GetAsync(Guid id)
        {
            return _planetRepository.GetAsync(id);
        }

        public Task<IEnumerable<Planet>> GetAllAsync()
        {
            return _planetRepository.GetAllAsync();
        }

        public Task<bool> UpdateAsync(Planet planet)
        {
            return _planetRepository.UpdateAsync(planet);
        }
    }
}
