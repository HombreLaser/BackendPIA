using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Errors;
using BackendPIA.Forms;
using BackendPIA.Logics;
using BackendPIA.Models;
using BackendPIA.Services;

namespace BackendPIA.Controllers {
    [Route("api/raffles/{raffleId:int}/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticket_service;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserAccount> _manager;

        public TicketsController(IMapper mapper, ITicketService service, ApplicationDbContext context, UserManager<UserAccount> manager) {
            _mapper = mapper;
            _ticket_service = service;
            _context = context;
            _manager = manager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> Index(long raffleId) {
            var result = await _ticket_service.GetTickets(raffleId);

            if(result == null)
                return StatusCode(404, new NotFoundError(404, "The resource doesn't exist"));

            return Ok(_mapper.Map<List<TicketDTO>>(result));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TicketDTO>> Show(long raffleId, long id) {
            var result = await _ticket_service.GetTicket(raffleId, id);

            if(result == null)
                return StatusCode(404, new NotFoundError(404, "The resource doesn't exist"));

            return Ok(_mapper.Map<TicketDTO>(result));
        }

        [Authorize]
        [HttpPost]
        public async Task <ActionResult<TicketDTO>> Create(long raffleId, TicketForm form) {
            string email = HttpContext.User.Claims.Where(c => c.Type.Contains("email")).First().Value;
            var user = await _manager.FindByEmailAsync(email);
            CreateTicketLogic logic = new CreateTicketLogic(_ticket_service, _context, _mapper, form, user, raffleId);
            var result = await logic.Call();

            if(!result)
                return StatusCode(422, new InvalidInputError(422, logic.ErrorMessage));
            
            return Ok(_mapper.Map<TicketDTO>(logic.Created));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id:int}")]
        public async Task <ActionResult> Delete(long raffleId, long id) {
            bool result = await _ticket_service.DeleteTicket(raffleId, id);

            if(!result)
                return StatusCode(404, new NotFoundError(404, "The ticket couldn't be found."));

            return Ok(new { Message = "The ticket has been deleted."});
        }
    }
}