using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceshipCargoTransport.Application.DTOs.Transport;
using SpaceshipCargoTransport.Domain.Models;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        private readonly ISpaceshipService _spaceshipService;
        private readonly IPlanetService _planetService;
        private readonly IMapper _mapper;

        public TransportController(ITransportService transportService, ISpaceshipService spaceshipService, IPlanetService planetService, IMapper mapper)
        {
            _transportService = transportService;
            _spaceshipService = spaceshipService;
            _planetService = planetService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns single transport with provided id.
        /// </summary>
        [HttpGet("{id}", Name = "GetTransport")]
        public async Task<ActionResult<TransportReadDTO>> GetTransport(Guid id)
        {
            var transport = await _transportService.GetDetailsAsync(id);

            if (transport == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TransportReadDTO>(transport));
        }

        /// <summary>
        /// Creates a transport with provided values.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TransportReadDTO>> Create([FromBody] TransportCreateDTO transportDTO)
        {
            var spaceship = await _spaceshipService.GetAsync(transportDTO.SpaceshipId);
            var startingLocation = await _planetService.GetAsync(transportDTO.StartingLocationId);
            var endingLocation = await _planetService.GetAsync(transportDTO.EndingLocationId);

            var transport = _mapper.Map<Transport>(transportDTO);

            transport.Spaceship = spaceship;
            transport.StartingLocation = startingLocation;
            transport.EndingLocation = endingLocation;

            if (await _transportService.RegisterNewAsync(transport))
            {
                var transportReadDTO = _mapper.Map<TransportReadDTO>(transport);
                return CreatedAtRoute(nameof(GetTransport), new { id = transportReadDTO.Id }, transportReadDTO);
            }

            return BadRequest();
        }

        /// <summary>
        /// Marks transport as being loaded.
        /// </summary>
        [HttpPost("{id}/cargo_loading", Name = "CargoLoadingTransport")]
        public async Task<ActionResult> CargoLoading(TransportCargoLoadingDTO transport)
        {
            if (await _transportService.SetToCargoLoadingAsync(transport.Id))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Marks transport as being in flight.
        /// </summary>
        [HttpPost("{id}/in_flight", Name = "InFlightTransport")]
        public async Task<ActionResult> InFlight(TransportInFlightDTO transport)
        {
            if (await _transportService.SetToInFlightAsync(transport.Id))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Marks transport as being unloaded.
        /// </summary>
        [HttpPost("{id}/cargo_unloading", Name = "CargoUnloadingTransport")]
        public async Task<ActionResult> CargoUnloading(TransportCargoUnloadingDTO transport)
        {
            if (await _transportService.SetToCargoUnloadingAsync(transport.Id))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Finishes a transport.
        /// </summary>
        [HttpPost("{id}/finish", Name = "FinishTransport")]
        public async Task<ActionResult> Finish(TransportFinishDTO transport)
        {
            if (await _transportService.SetToFinishedAsync(transport.Id))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Marks transport as lost.
        /// </summary>
        [HttpPost("{id}/lost", Name = "LostTransport")]
        public async Task<ActionResult> Lost(TransportLostDTO transport)
        {
            if (await _transportService.SetToLostAsync(transport.Id))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Cancels a transport.
        /// </summary>
        [HttpPost("{id}/cancel", Name = "CancelTransport")]
        public async Task<ActionResult> Cancel(TransportCancelDTO transport)
        {
            if (await _transportService.CancelAsync(transport.Id))
                return Ok();

            return BadRequest();
        }
    }
}
