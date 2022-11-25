using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface IGameService {
        public Task<IEnumerable<RaffleWinner>> GetWinners(long raffle_id);
    }
}