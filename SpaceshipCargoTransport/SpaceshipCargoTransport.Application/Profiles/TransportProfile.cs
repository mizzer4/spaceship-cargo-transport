using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.Transport;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Application.Profiles
{
    public class TransportProfile : Profile
    {
        public TransportProfile()
        {
            // Source -> Target
            CreateMap<Transport, TransportReadDTO>()
                .ForMember(dest => dest.SpaceshipId, opt => opt.MapFrom(src => src.Spaceship.Id))
                .ForMember(dest => dest.StartingLocationId, opt => opt.MapFrom(src => src.StartingLocation.Id))
                .ForMember(dest => dest.EndingLocationId, opt => opt.MapFrom(src => src.EndingLocation.Id));
            CreateMap<TransportCreateDTO, Transport>();
        }
    }
}