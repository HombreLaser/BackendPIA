using AutoMapper;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics {
    public class CreateTicketLogic {
        private readonly ITicketService _ticket_service;
        private readonly ApplicationDbContext _context;
        private readonly UserAccount _user;
        private readonly long _raffle_id;
        private readonly IMapper _mapper;
        private readonly TicketForm _form;
        public string? ErrorMessage { get; set; }
        public Ticket? Created { get; set; }
        
        public CreateTicketLogic(ITicketService service, ApplicationDbContext context, IMapper mapper, TicketForm form, UserAccount user, long raffle_id) {
            _ticket_service = service;
            _mapper = mapper;
            _form = form;
            _user = user;
            _raffle_id = raffle_id;
            _context = context;
        }

        public async Task<bool> Call() {
            var raffle = _context.Raffles.Find(_raffle_id);

            // Check if the user exists.
            if(_user == null)
                return false;

            // Check if the given raffle exists-
            if(raffle == null) {
                ErrorMessage = "The raffle doesn't exist.";

                return false;
            }

            // Check if the raffle has already closed.
            if(raffle.IsClosed) {
                ErrorMessage = "The raffle is already closed.";

                return false;
            }

            // Check if the user already has a ticket for the given raffle.
            if(_context.Tickets.Where(t => t.RaffleId == _raffle_id).Where(t => t.UserAccountId == _user.Id).Count() > 0) {
                ErrorMessage = $"There's already a ticket for {_user.UserName}.";

                return false;
            }

            // Check if the number has been taken.
            if(_context.Tickets.Where(t => t.RaffleId == _raffle_id).Where(t => t.Number == _form.Number).Count() > 0) {
                ErrorMessage = $"There's already a registered ticket with the number {_form.Number}.";

                return false;
            }

            var ticket = _mapper.Map<Ticket>(_form);
            ticket.RaffleId = _raffle_id;

            // Check if the given raffle has reached the ticket limit.
            if(_context.Tickets.Where(t => t.RaffleId == _raffle_id).Count() >= 54) {
                ErrorMessage = $"The raffle with id {_raffle_id} has reached the ticket limit";

                return false;
            }
            
            ticket.UserAccountId = _user.Id;
            ticket.IsWinner = false;
            Created = await _ticket_service.CreateTicket(ticket);

            return true;
        }
    }
}