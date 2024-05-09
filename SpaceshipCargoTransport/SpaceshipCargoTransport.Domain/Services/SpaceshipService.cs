using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;

namespace SpaceshipCargoTransport.Domain.Services
{
    public class SpaceshipService : ISpaceshipService
    {
        private readonly ISpaceshipRepository _spaceshipRepository;

        public SpaceshipService(ISpaceshipRepository spaceshipRepository)
        {
            _spaceshipRepository = spaceshipRepository;
        }

        public Task<bool> CreateAsync(Spaceship ship)
        {
            ship.Id = Guid.NewGuid();

            return _spaceshipRepository.CreateAsync(ship);
        }

        public Task<bool> DeleteAsync(Spaceship ship)
        {
            return _spaceshipRepository.DeleteAsync(ship);
        }

        public Task<Spaceship?> GetAsync(Guid id)
        {
            return _spaceshipRepository.GetAsync(id);
        }

        public Task<IEnumerable<Spaceship>> GetAllAsync()
        {
            return _spaceshipRepository.GetAllAsync();
        }

        public Task<bool> UpdateAsync(Spaceship ship)
        {
            return _spaceshipRepository.UpdateAsync(ship);
        }
    }
}
