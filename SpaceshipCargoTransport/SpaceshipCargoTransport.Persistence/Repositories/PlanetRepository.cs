using Microsoft.EntityFrameworkCore;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Persistence.Db;

namespace SpaceshipCargoTransport.Persistence.Repositories
{
    internal class PlanetRepository : IPlanetRepository
    {
        private readonly AppDbContext _context;

        public PlanetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Planet planet)
        {
            await _context.Planets.AddAsync(planet);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Planet planet)
        {
            _context.Planets.Remove(planet);
            return await SaveChangesAsync();
        }

        public async Task<Planet?> GetAsync(Guid id)
        {
            return await _context.Planets
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Planet>> GetAllAsync()
        {
            return await _context.Planets.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Planet planet)
        {
            _context.Planets.Update(planet);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}