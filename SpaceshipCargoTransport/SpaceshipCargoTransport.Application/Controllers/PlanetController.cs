using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;

namespace PlanetCargoTransport.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetService _planetService;
        private readonly IMapper _mapper;

        public PlanetController(IPlanetService planetService, IMapper mapper)
        {
            _planetService = planetService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all planets.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanetReadDTO>>> GetAllPlanets()
        {
            var planets = await _planetService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PlanetReadDTO>>(planets));
        }

        /// <summary>
        /// Returns single planet with provided id.
        /// </summary>
        [HttpGet("{id}", Name = "GetPlanet")]
        public async Task<ActionResult<PlanetReadDTO>> GetPlanet(Guid id)
        {
            var planet = await _planetService.GetAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlanetReadDTO>(planet));
        }

        /// <summary>
        /// Creates a planet with provided values.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PlanetReadDTO>> Create([FromBody] PlanetCreateDTO PlanetDTO)
        {
            var planet = _mapper.Map<Planet>(PlanetDTO);

            if (await _planetService.CreateAsync(planet))
            {
                var planetReadDTO = _mapper.Map<PlanetReadDTO>(planet);
                return CreatedAtRoute(nameof(GetPlanet), new { id = planet.Id }, planetReadDTO);
            }
                

            return BadRequest();
        }

        /// <summary>
        /// Updates a planet with given values.
        /// </summary>
        [HttpPut("{id}", Name = "UpdatePlanet")]
        public async Task<ActionResult<Planet>> UpdatePlanet([FromBody] PlanetUpdateDTO planet)
        {
            if (await _planetService.UpdateAsync(_mapper.Map<Planet>(planet)))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Deletes a planet.
        /// </summary>
        [HttpDelete("{id}", Name = "DeletePlanet")]
        public async Task<ActionResult> DeletePlanet([FromRoute] PlanetDeleteDTO planet)
        {
            if (await _planetService.DeleteAsync(_mapper.Map<Planet>(planet)))
                return Ok();

            return BadRequest();
        }
    }
}
