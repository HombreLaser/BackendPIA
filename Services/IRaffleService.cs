using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface IRaffleService {
        public Task<Raffle> CreateRaffle(Raffle to_create);
        public Task<Raffle> UpdateRaffle(Raffle to_update);
        public Task<IEnumerable<Raffle>> GetRaffles(string query);
        public Task<Raffle> GetRaffle(long id);
        public Task<bool> DeleteRaffle(long id);
        public Task<IEnumerable<int>> GetTakenTickets(long id);
        public IEnumerable<Ticket> GetRaffleTickets(long id);
        public Task<IEnumerable<RaffleWinner>> GetRaffleWinners(long id);
        public bool RaffleExists(long id);
    }
}