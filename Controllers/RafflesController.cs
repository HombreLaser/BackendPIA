using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Forms;
using BackendPIA.Models;
using BackendPIA.Services;
using BackendPIA.Logics;

namespace BackendPIA.Controllers {
    [Route("api/raffles")]
    [ApiController]
    public class RafflesController : ControllerBase {
        private readonly IRaffleService _service;
        private readonly IMapper _mapper;

        public RafflesController(IMapper mapper, IRaffleService service) {
            _service = service;
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
        public async Task<ActionResult> Update(long id) {
            var result = await _service.DeleteRaffle(id);

            if(!result)
                return NotFound("The resource couldn't be found.");

            return StatusCode(303, new { Message = "The resource has been deleted"} );
        }
    }
}