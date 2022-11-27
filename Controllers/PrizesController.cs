using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Errors;
using BackendPIA.Forms;
using BackendPIA.Models;
using BackendPIA.Services;

namespace BackendPIA.Controllers {
    [Route("api/prizes")]
    [ApiController]
    public class PrizesController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IPrizeService _prize_service;
        private readonly ApplicationDbContext _context;

        public PrizesController(IMapper mapper, IPrizeService service, ApplicationDbContext context) {
            _mapper = mapper;
            _prize_service = service;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Prize>>> Index() {
            var result = await _prize_service.GetPrizes();
            
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Prize>> Show(long id) {
            var result = await _prize_service.GetPrize(id);

            if(result == null)
                return NotFound(new NotFoundError(404, $"The resource with id {id} couldn't be found"));

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator", Policy = "ValidToken")]
        public async Task<ActionResult<Prize>> Create(PrizeForm form) {
            if(!_context.Raffles.Any(r => r.Id == form.RaffleId))
                return BadRequest(new NotFoundError(404, $"The raffle with id {form.RaffleId} couldn't be found"));

            var to_create = _mapper.Map<Prize>(form);
            var result = await _prize_service.CreatePrize(to_create);

            return Ok(result);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Prize>> Update([FromBody] JsonPatchDocument<PrizeForm> patch_doc_form, long id) {
            if(patch_doc_form == null)
                return BadRequest(ModelState);
            
            var patch_doc = _mapper.Map<JsonPatchDocument<Prize>>(patch_doc_form);
            var prize = _context.Prizes.Find(id);

            if(prize == null)
                return NotFound(new NotFoundError(404, $"The resource with id {id} couldn't be found"));

            patch_doc.ApplyTo(prize, ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            prize.Raffle = _context.Raffles.Find(prize.RaffleId);
            return Ok(prize);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(long id) {
            var result = await _prize_service.DeletePrize(id);

            if(!result)
                return NotFound($"The prize with id {id} couldnt be found.");

            return StatusCode(303, new { Message = $"The prize with id {id} has been deleted."});
        }
    }
}