using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Forms;
using BackendPIA.Errors;
using BackendPIA.Logics;
using BackendPIA.Models;
using BackendPIA.Services;

namespace BackendPIA.Controllers {
    [Route("api/raffles")]
    [ApiController]
    public class RafflesController : ControllerBase {
        private readonly IRaffleService _service;
        private readonly IGameService _game_service;
        private readonly IMapper _mapper;

        public RafflesController(IMapper mapper, IRaffleService service, IGameService game_service) {
            _service = service;
            _game_service = game_service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Raffle>> Index([FromQuery] string name = "") {
            return await _service.GetRaffles(name);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Raffle>> Show(long id) {    
            var raffle = await _service.GetRaffle(id);

            if(raffle == null)
                return NotFound("The resource couldn't be found");

            return raffle;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Raffle>> Create(RaffleForm form) {
            var raffle = await _service.CreateRaffle(_mapper.Map<Raffle>(form));

            return raffle;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Raffle>> Update(long id, RaffleForm form) {
            var raffle = _mapper.Map<Raffle>(form);
            raffle.Id = id;
            var result = await _service.UpdateRaffle(raffle);

            if(result == null)
                return NotFound("The resource couldn't be found.");

            return raffle;
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(long id) {
            var result = await _service.DeleteRaffle(id);

            if(!result)
                return NotFound("The resource couldn't be found.");

            return StatusCode(303, new { Message = "The resource has been deleted"} );
        }

        [Authorize]
        [HttpGet("{id:int}/available_tickets")]
        public async Task<ActionResult<IEnumerable<int>>> AvailableTickets(long id) {
            IEnumerable<int> available_tickets = from number in Enumerable.Range(1, 54) select number;
            IEnumerable<int> taken_tickets = await _service.GetTakenTickets(id);

            if(!taken_tickets.Any())
                return NotFound(new NotFoundError(404, $"The raffle with id {id} doesn't exist or doesn't have any tickets."));

            return Ok(new { Numbers = available_tickets.Except(taken_tickets) });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("{id:int}/play")]
        public async Task<ActionResult<IEnumerable<WinnerDTO>>> Play(long id) {
            RafflePlayLogic logic = new RafflePlayLogic(_game_service, _service, id);
            bool result = await logic.Call();
            
            if(!result)
                return BadRequest(new { ErrorMessage = logic.ErrorMessage });

            return Ok(_mapper.Map<IEnumerable<WinnerDTO>>(logic.Winners));
        }

        [Authorize]
        [HttpGet("{id:int}/winners")]
        public async Task<ActionResult<IEnumerable<WinnerDTO>>> GetWinners(long id) {
            var raffle = await _service.GetRaffle(id);

            if(raffle == null)
                return NotFound(new NotFoundError(404, $"The raffle with id {id} doesn't exist."));

            if(!raffle.IsClosed)
                return NotFound(new NotFoundError(404, $"The raffle with id {id} doesn't have any winners yet."));

            var result = await _service.GetRaffleWinners(id);

            return Ok(_mapper.Map<IEnumerable<WinnerDTO>>(result));
        }
    }
}