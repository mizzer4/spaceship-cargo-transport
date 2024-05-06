using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Application.Profiles
{
    public class PlanetProfile : Profile
    {
        public PlanetProfile()
        {
            // Source -> Target
            CreateMap<Planet, PlanetReadDTO>();
            CreateMap<PlanetCreateDTO, Planet>();
            CreateMap<PlanetUpdateDTO, Planet>();
            CreateMap<PlanetDeleteDTO, Planet>();
        }
    }
}