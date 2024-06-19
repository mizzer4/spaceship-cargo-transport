using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.Authentication;
using SpaceshipCargoTransport.Application.DTOs.PlanetDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;
using System.Numerics;

namespace PlanetCargoTransport.Application.Controllers
{
    /// <summary>
    /// Controller handling Planet endpoints.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetService _planetService;


        public PlanetController(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        /// <summary>
        /// Returns all planets.
        /// </summary>       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlanetReadDTO>>> GetAllPlanets()
        {
            var planets = await _planetService.GetAllAsync();

            return Ok(planets);
        }

        /// <summary>
        /// Returns single planet with provided id.
        /// </summary>       
        [HttpGet("{id}", Name = "GetPlanet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanetReadDTO>> GetPlanet(Guid id)
        {
            var planet = await _planetService.GetAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            return Ok(planet);
        }

        /// <summary>
        /// Creates a planet with provided values.
        /// </summary>       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlanetReadDTO>> Create([FromBody] PlanetCreateDTO PlanetDTO)
        {
            var planetReadDTO = await _planetService.CreateAsync(PlanetDTO);

            if (planetReadDTO == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute(nameof(GetPlanet), new { id = planetReadDTO.Id }, planetReadDTO);
        }

        /// <summary>
        /// Updates a planet with given values.
        /// </summary>        
        [HttpPut("{id}", Name = "UpdatePlanet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Planet>> UpdatePlanet([FromQuery] Guid id, [FromBody] PlanetUpdateDTO planetDTO)
        {
            var planet = await _planetService.GetAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            var isSuccessfull = await _planetService.UpdateAsync(planetDTO);

            if (!isSuccessfull)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a planet.
        /// </summary>
        [HttpDelete("{id}", Name = "DeletePlanet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePlanet(Guid id)
        {
            var planet = await _planetService.GetAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            var isSuccessfull = await _planetService.DeleteAsync(id);

            if (!isSuccessfull)
            {
                return BadRequest();
            }
 
            return Ok();
        }
    }
}
