using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Repositories
{
    public interface ISpaceshipRepository
    {
        Task<Spaceship?> GetAsync(Guid id);
        Task<IEnumerable<Spaceship>> GetAllAsync();
        Task<bool> CreateAsync(Spaceship ship);
        Task<bool> UpdateAsync(Spaceship ship);
        Task<bool> DeleteAsync(Guid id);
    }
}
