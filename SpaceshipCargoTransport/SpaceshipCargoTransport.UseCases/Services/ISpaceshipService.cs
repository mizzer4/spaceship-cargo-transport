using SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ISpaceshipService
    {
        Task<SpaceshipReadDTO?> GetAsync(Guid id);
        Task<IEnumerable<SpaceshipReadDTO>> GetAllAsync();
        Task<SpaceshipReadDTO?> CreateAsync(SpaceshipCreateDTO ship);
        Task<bool> UpdateAsync(SpaceshipUpdateDTO ship);
        Task<bool> DeleteAsync(Guid id);
    }                    
}
