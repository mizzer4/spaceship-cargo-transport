using Microsoft.EntityFrameworkCore;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Persistence.Db;

namespace SpaceshipCargoTransport.Persistence.Repositories
{
    internal class TransportRepository : ITransportRepository
    {
        private readonly AppDbContext _context;

        public TransportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Transport transport)
        {
            await _context.Transports.AddAsync(transport);
            return await SaveChangesAsync();
        }

        public async Task<Transport?> GetAsync(Guid id)
        {
            return await _context.Transports
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Transport transport)
        {
            _context.Transports.Update(transport);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}