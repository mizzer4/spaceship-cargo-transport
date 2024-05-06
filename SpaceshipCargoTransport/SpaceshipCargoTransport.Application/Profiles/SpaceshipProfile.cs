using AutoMapper;
using SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Application.Profiles
{
    public class SpaceshipProfile : Profile
    {
        public SpaceshipProfile()
        {
            // Source -> Target
            CreateMap<Spaceship, SpaceshipReadDTO>();
            CreateMap<SpaceshipCreateDTO, Spaceship>();
            CreateMap<SpaceshipUpdateDTO, Spaceship>();
            CreateMap<SpaceshipDeleteDTO, Spaceship>();
        }
    }
}