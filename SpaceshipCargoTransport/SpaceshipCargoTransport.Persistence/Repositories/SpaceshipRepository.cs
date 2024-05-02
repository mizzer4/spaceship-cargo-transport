using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Persistence.Db;

namespace SpaceshipCargoTransport.Persistence.Repositories
{
    internal class SpaceshipRepository : ISpaceshipRepository
    {
        private readonly AppDbContext _context;

        public SpaceshipRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(Spaceship ship)
        {
            _context.Spaceships.Add(ship);
            return SaveChanges();
        }

        public bool Delete(Spaceship ship)
        {
            _context.Spaceships.Remove(ship);
            return SaveChanges();
        }

        public Spaceship? Get(Guid id)
        {
            return _context.Spaceships
                .Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Spaceship> GetAll()
        {
            return _context.Spaceships.ToList();
        }

        public bool Update(Spaceship ship)
        {
            _context.Spaceships.Update(ship);
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}