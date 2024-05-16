using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.Transport;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Application.Profiles
{
    public class TransportProfile : Profile
    {
        private readonly ISpaceshipService _spaceshipService;
        private readonly IPlanetService _planetService;

        public TransportProfile(ISpaceshipService spaceshipService, IPlanetService planetService)
        {
            _spaceshipService = spaceshipService;
            _planetService = planetService;
        }

        public TransportProfile()
        {
            // Source -> Target
            CreateMap<Transport, TransportReadDTO>()
                .ForMember(dest => dest.SpaceshipId, opt => opt.MapFrom(src => src.Spaceship.Id))
                .ForMember(dest => dest.StartingLocationId, opt => opt.MapFrom(src => src.StartingLocation.Id))
                .ForMember(dest => dest.EndingLocationId, opt => opt.MapFrom(src => src.EndingLocation.Id));
            CreateMap<TransportCreateDTO, Transport>()
                .ForMember(dest => dest.StartingLocation, opt => opt.MapFrom(src => _planetService.GetAsync(src.StartingLocationId)))
                .ForMember(dest => dest.EndingLocation, opt => opt.MapFrom(src => _planetService.GetAsync(src.EndingLocationId)))
                .ForMember(dest => dest.Spaceship, opt => opt.MapFrom(src => _spaceshipService.GetAsync(src.SpaceshipId)));
        }
    }
}