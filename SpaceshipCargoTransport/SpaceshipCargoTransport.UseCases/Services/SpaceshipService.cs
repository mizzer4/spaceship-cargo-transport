using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Repositories;

namespace SpaceshipCargoTransport.Domain.Services
{
    public class SpaceshipService : ISpaceshipService
    {
        private readonly ISpaceshipRepository _spaceshipRepository;
        private readonly IMapper _mapper;

        public SpaceshipService(ISpaceshipRepository spaceshipRepository, IMapper mapper)
        {
            _spaceshipRepository = spaceshipRepository;
            _mapper = mapper;
        }

        public async Task<SpaceshipReadDTO?> CreateAsync(SpaceshipCreateDTO spaceshipDTO)
        {
            var spaceship = _mapper.Map<Spaceship>(spaceshipDTO);
            spaceship.Id = Guid.NewGuid();

            if (await _spaceshipRepository.CreateAsync(spaceship))
            {
                return _mapper.Map<SpaceshipReadDTO>(spaceship);
            }

            return null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _spaceshipRepository.DeleteAsync(id);
        }

        public async Task<SpaceshipReadDTO?> GetAsync(Guid id)
        {
            var spaceship =  await _spaceshipRepository.GetAsync(id);
            return _mapper.Map<SpaceshipReadDTO>(spaceship);
        }

        public async Task<IEnumerable<SpaceshipReadDTO>> GetAllAsync()
        {
            var spaceships = await _spaceshipRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpaceshipReadDTO>>(spaceships);
        }

        public Task<bool> UpdateAsync(SpaceshipUpdateDTO spaceshipDTO)
        {
            var spaceship = _mapper.Map<Spaceship>(spaceshipDTO);
            return _spaceshipRepository.UpdateAsync(spaceship);
        }
    }
}
