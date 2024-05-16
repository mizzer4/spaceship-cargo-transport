using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ISpaceshipService
    {
        Task<Spaceship?> GetAsync(Guid id);
        Task<IEnumerable<Spaceship>> GetAllAsync();
        Task<bool> CreateAsync(Spaceship ship);
        Task<bool> UpdateAsync(Spaceship ship);
        Task<bool> DeleteAsync(Guid id);
    }                    
}
