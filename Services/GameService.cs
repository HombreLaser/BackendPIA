using Microsoft.EntityFrameworkCore;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class GameService : IGameService {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<RaffleWinner>> GetWinners(long raffle_id) {
            var raffle = await _context.Raffles.Include(r => r.Tickets).ThenInclude(t => t.Owner).FirstOrDefaultAsync(r => r.Id == raffle_id);
            Queue<long> prizes = new Queue<long>(_context.Prizes.Where(p => p.RaffleId == raffle_id).OrderBy(p => p.Tier).Select(p => p.Id));
            // var tickets = await _context.Tickets.Where(t => t.RaffleId == raffle_id).Select(t => t.Number).ToListAsync();
            List<int> winners = GetTicketNumbers(raffle.Winners, raffle.Tickets);

            foreach(int winner in winners) {
                var raffle_winner = new RaffleWinner { UserAccountId = raffle.Tickets.Where(t => t.Number == winner).First().UserAccountId,
                                                       PrizeId = prizes.Dequeue(), RaffleId = raffle_id };
                await _context.AddAsync(raffle_winner);
            }

            raffle.IsClosed = true;
            await _context.SaveChangesAsync();

            return await _context.RaffleWinners.Include(rw => rw.UserAccount).Include(rw => rw.Raffle).Include(rw => rw.Prize)
                                                .Where(rw => rw.RaffleId == raffle_id).ToListAsync();
        }

        private List<int> GetTicketNumbers(int limit, ICollection<Ticket> tickets) {
            List<int> winners = new List<int>();
            Random rng = new Random();

            while(winners.Count < limit) {
                int i = rng.Next(tickets.Count - 1);

                if(winners.Contains(tickets.ElementAt(i).Number)) // Check for duplicate numbers. 
                    continue;

                winners.Add(tickets.ElementAt(i).Number);
                tickets.ElementAt(i).IsWinner = true;
            }

            return winners;
        }
    }
}