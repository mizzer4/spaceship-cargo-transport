using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.DTOs.Spaceship;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Application.Controllers
{
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
        public async Task<ActionResult<IEnumerable<SpaceshipReadDTO>>> GetAll()
        {
            var spaceships = await _spaceshipService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<SpaceshipReadDTO>>(spaceships));
        }

        /// <summary>
        /// Returns single spaceship with provided id.
        /// </summary>
        [HttpGet("{id}", Name = "GetSpaceship")]
        public async Task<ActionResult<SpaceshipReadDTO>> Get(Guid id)
        {
            var spaceship = await _spaceshipService.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpaceshipReadDTO>(spaceship));
        }

        /// <summary>
        /// Creates a spaceship with provided values.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SpaceshipReadDTO>> Create([FromBody] SpaceshipCreateDTO spaceshipDTO)
        {
            var spaceship = _mapper.Map<Spaceship>(spaceshipDTO);

            if (await _spaceshipService.CreateAsync(spaceship))
                return CreatedAtRoute(nameof(Get), new { spaceship.Id }, spaceship);

            return BadRequest();
        }

        /// <summary>
        /// Updates a spaceship with given values.
        /// </summary>
        [HttpPut("{id}", Name = "UpdateSpaceship")]
        public async Task<ActionResult<Spaceship>> Update([FromBody] SpaceshipUpdateDTO spaceship)
        {
            if (await _spaceshipService.UpdateAsync(_mapper.Map<Spaceship>(spaceship)))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Deletes a spaceship.
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteSpaceship")]
        public async Task<ActionResult<Spaceship>> Delete([FromBody] SpaceshipDeleteDTO spaceship)
        {
            if (await _spaceshipService.DeleteAsync(_mapper.Map<Spaceship>(spaceship)))
                return Ok();

            return BadRequest();
        }
    }
}
