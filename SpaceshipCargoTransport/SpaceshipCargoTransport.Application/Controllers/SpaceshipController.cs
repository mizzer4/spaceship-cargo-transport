using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.Authentication;
using SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Application.Controllers
{
    /// <summary>
    /// Controller handling Spaceship endpoints.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceshipController : ControllerBase
    {
        private readonly ISpaceshipService _spaceshipService;
        private readonly IMapper _mapper;

        public SpaceshipController(ISpaceshipService spaceshipService, IMapper mapper)
        {
            _spaceshipService = spaceshipService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all spaceships.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpaceshipReadDTO>>> GetAllSpaceships()
        {
            var spaceships = await _spaceshipService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<SpaceshipReadDTO>>(spaceships));
        }

        /// <summary>
        /// Returns single spaceship with provided id.
        /// </summary>
        [HttpGet("{id}", Name = "GetSpaceship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpaceshipReadDTO>> GetSpaceship(Guid id)
        {

            var spaceship = await _spaceshipService.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            return Ok(spaceship);
        }

        /// <summary>
        /// Creates a spaceship with provided values.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpaceshipReadDTO>> CreateSpaceship([FromBody] SpaceshipCreateDTO spaceshipDTO)
        {
            var spaceshipReadDTO = await _spaceshipService.CreateAsync(spaceshipDTO);

            if (spaceshipReadDTO == null)
            {
                return BadRequest();             
            }

            return CreatedAtRoute(nameof(GetSpaceship), new { id = spaceshipReadDTO.Id }, spaceshipReadDTO);
        }

        /// <summary>
        /// Updates a spaceship with given values.
        /// </summary>
        [HttpPut("{id}", Name = "UpdateSpaceship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Spaceship>> UpdateSpaceship([FromQuery] Guid id, [FromBody] SpaceshipUpdateDTO spaceshipDTO)
        {
            var spaceship = await _spaceshipService.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var isSuccessfull = await _spaceshipService.UpdateAsync(spaceshipDTO);

            if (!isSuccessfull)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a spaceship.
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteSpaceship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteSpaceship(Guid id)
        {
            var spaceship = await _spaceshipService.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            if (await _spaceshipService.DeleteAsync(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
