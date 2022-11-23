using Microsoft.EntityFrameworkCore;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class TicketService : ITicketService {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Ticket> CreateTicket(Ticket to_create) {
            await _context.AddAsync(to_create);
            await _context.SaveChangesAsync();

            return to_create;
        }

        public async Task<IEnumerable<Ticket>> GetTickets(long raffle_id) {
            return await _context.Tickets.Where(t => t.RaffleId == raffle_id).ToListAsync();
        }

        public async Task<Ticket> GetTicket(long raffle_id, long id) {
            return await _context.Tickets.Where(t => t.RaffleId == raffle_id).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> DeleteTicket(long raffle_id, long id) {
            var to_delete = await _context.Tickets.Where(t => t.RaffleId == raffle_id).FirstOrDefaultAsync(t => t.Id == id);

            if(to_delete == null)
                return false;

            _context.Tickets.Remove(to_delete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}