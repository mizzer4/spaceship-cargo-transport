using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface IPlanetService
    {
        Task<PlanetReadDTO?> GetAsync(Guid id);
        Task<IEnumerable<PlanetReadDTO>> GetAllAsync();
        Task<PlanetReadDTO> CreateAsync(PlanetCreateDTO planet);
        Task<bool> UpdateAsync(PlanetUpdateDTO planet);
        Task<bool> DeleteAsync(Guid id);
    }
}
