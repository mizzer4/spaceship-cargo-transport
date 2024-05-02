using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;

namespace SpaceshipCargoTransport.Domain.Services
{
    internal class SpaceshipService : ISpaceshipService
    {
        private readonly ISpaceshipRepository _spaceshipRepository;

        public SpaceshipService(ISpaceshipRepository spaceshipRepository)
        {
            _spaceshipRepository = spaceshipRepository;
        }

        public bool Create(Spaceship ship)
        {
            ship.Id = Guid.NewGuid();

            return _spaceshipRepository.Create(ship);
        }

        public bool Delete(Spaceship ship)
        {
            return _spaceshipRepository.Delete(ship);
        }

        public Spaceship? Get(Guid id)
        {
            return _spaceshipRepository.Get(id);
        }

        public IEnumerable<Spaceship> GetAll()
        {
            return _spaceshipRepository.GetAll();
        }

        public bool Update(Spaceship ship)
        {
            return _spaceshipRepository.Update(ship);
        }
    }
}
