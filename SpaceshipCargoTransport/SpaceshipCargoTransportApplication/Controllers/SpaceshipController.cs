using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.DTOs;
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
        /// This endpoint returns all spaceships.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceshipReadDTO>>> GetAll()
        {
            var spaceships = _spaceshipService.GetAll();

            return Ok(_mapper.Map<SpaceshipReadDTO>(spaceships));
        }

        /// <summary>
        /// This endpoint returns single spaceship with provided id.
        /// </summary>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<SpaceshipReadDTO>> Get(Guid id)
        {
            var spaceship = _spaceshipService.Get(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpaceshipReadDTO>(spaceship));
        }

        /// <summary>
        /// This endpoint creates a spaceship with provided values.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SpaceshipReadDTO>> Create([FromBody] SpaceshipCreateDTO newSpaceship)
        {
            var spaceship = _mapper.Map<Spaceship>(newSpaceship);

            if (_spaceshipService.Create(spaceship))
                return CreatedAtRoute(nameof(Get), new { spaceship.Id }, spaceship);

            return BadRequest();
        }

        /// <summary>
        /// This endpoint updates a spaceship with given values.
        /// </summary>
        [HttpPut("{id}", Name = "Update")]
        public async Task<ActionResult<Spaceship>> Update([FromBody] SpaceshipUpdateDTO spaceship)
        {
            if (_spaceshipService.Update(_mapper.Map<Spaceship>(spaceship)))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// This endpoint deletes a spaceship.
        /// </summary>
        [HttpDelete("{id}", Name = "Delete")]
        public async Task<ActionResult<Spaceship>> Delete([FromBody] SpaceshipDeleteDTO spaceship)
        {
            if (_spaceshipService.Delete(_mapper.Map<Spaceship>(spaceship)))
                return Ok();

            return BadRequest();
        }
    }
}
