using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CreateAsync(Spaceship ship)
        {
            await _context.Spaceships.AddAsync(ship);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Spaceship ship)
        {
            _context.Spaceships.Remove(ship);
            return await SaveChangesAsync();
        }

        public async Task<Spaceship?> GetAsync(Guid id)
        {
            return await _context.Spaceships
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Spaceship>> GetAllAsync()
        {
            return await _context.Spaceships.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Spaceship ship)
        {
            _context.Spaceships.Update(ship);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}