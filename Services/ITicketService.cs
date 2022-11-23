using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface ITicketService {
        public Task<Ticket> CreateTicket(Ticket to_create);
        public Task<IEnumerable<Ticket>> GetTickets(long raffle_id);
        public Task<Ticket> GetTicket(long raffle_id, long id);
        public Task<bool> DeleteTicket(long raffle_id, long id);
    }
}