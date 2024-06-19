using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Repositories
{
    public interface ITransportRepository
    {
        Task<Transport?> GetAsync(Guid id);
        Task<bool> CreateAsync(Transport transport);
        Task<bool> UpdateAsync(Transport transport);
    }
}
