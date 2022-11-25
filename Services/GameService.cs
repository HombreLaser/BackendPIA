using Microsoft.EntityFrameworkCore;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class GameService : IGameService {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<RaffleWinner>> GetWinners(long raffle_id) {
            var raffle = await _context.Raffles.Include(r => r.Tickets.Select(t => t.Owner)).FirstOrDefaultAsync(r => r.Id == raffle_id);
            Queue<long> prizes = new Queue<long>(_context.Prizes.Where(p => p.RaffleId == raffle_id).Select(p => p.Id));
            var tickets = await _context.Tickets.Where(t => t.RaffleId == raffle_id).Select(t => t.Number).ToListAsync();
            List<int> winners = GetTicketNumbers(raffle.Winners, tickets);
            List<RaffleWinner> raffle_winners = new List<RaffleWinner>();

            foreach(int winner in winners) {
                var raffle_winner = new RaffleWinner { UserAccountId = raffle.Tickets.Where(t => t.Number == winner).First().UserAccountId,
                                                       PrizeId = prizes.Dequeue(), RaffleId = raffle_id };
                raffle_winners.Add(raffle_winner);
                await _context.AddAsync(raffle_winner);
            }

            await _context.SaveChangesAsync();

            return raffle_winners;
        }

        private List<int> GetTicketNumbers(int limit, List<int> tickets) {
            List<int> winners = new List<int>();
            Random rng = new Random();

            while(winners.Count < limit) {
                int i = rng.Next(tickets.Count - 1);

                if(winners.Contains(tickets[i])) // Check for duplicate numbers. 
                    continue;

                winners.Append(tickets[i]);
            }

            return winners;
        }
    }
}