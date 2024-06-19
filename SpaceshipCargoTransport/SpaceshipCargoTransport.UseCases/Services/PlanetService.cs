using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;
using System.Numerics;

namespace SpaceshipCargoTransport.Domain.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IMapper _mapper;

        public PlanetService(IPlanetRepository planetRepository, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _mapper = mapper;
        }

        public async Task<PlanetReadDTO?> CreateAsync(PlanetCreateDTO planetDTO)
        {
            var planet = _mapper.Map<Planet>(planetDTO);
            planet.Id = Guid.NewGuid();

            if (await _planetRepository.CreateAsync(planet))
            {
                return _mapper.Map<PlanetReadDTO>(planet);
            }

            return null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _planetRepository.DeleteAsync(id);
        }

        public async Task<PlanetReadDTO?> GetAsync(Guid id)
        {
            var planet = await _planetRepository.GetAsync(id);
            return _mapper.Map<PlanetReadDTO>(planet);
        }

        public async Task<IEnumerable<PlanetReadDTO>> GetAllAsync()
        {
            var planets = await _planetRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlanetReadDTO>>(planets);
        }

        public Task<bool> UpdateAsync(PlanetUpdateDTO planetDTO)
        {
            var planet = _mapper.Map<Planet>(planetDTO);
            return _planetRepository.UpdateAsync(planet);
        }
    }
}
