﻿using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface IPlanetService
    {
        Task<Planet?> GetAsync(Guid id);
        Task<IEnumerable<Planet>> GetAllAsync();
        Task<bool> CreateAsync(Planet planet);
        Task<bool> UpdateAsync(Planet planet);
        Task<bool> DeleteAsync(Guid id);
    }
}
