using Microsoft.EntityFrameworkCore;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Services {
    public class RaffleService : IRaffleService {
        private readonly ApplicationDbContext _context;

        public RaffleService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Raffle> CreateRaffle(Raffle to_create) {
            to_create.IsClosed = false;

            await _context.AddAsync(to_create);
            await _context.SaveChangesAsync();

            return to_create;
        }

        public async Task<Raffle> UpdateRaffle(Raffle to_update) {
            bool it_exists = _context.Raffles.Any(r => r.Id == to_update.Id);

            if(!it_exists)
                return null;

            _context.Update(to_update);
            await _context.SaveChangesAsync();

            return to_update;
        }

        public async Task<IEnumerable<Raffle>> GetRaffles(string query) {
            if(String.IsNullOrEmpty(query))
                return await _context.Raffles.ToListAsync();
            
            return _context.Raffles.FromSql($"SELECT * FROM \"Raffles\" AS r WHERE r.\"Name\" ILIKE {query}").ToList();
        }

        public async Task<Raffle> GetRaffle(long id) {
            return await _context.Raffles.Include(r => r.Prizes).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> DeleteRaffle(long id) {
            var to_delete = await _context.Raffles.FindAsync(id);

            if(to_delete == null)
                return false;

            _context.Raffles.Remove(to_delete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<int>> GetTakenTickets(long id) {
            return await _context.Tickets.Where(t => t.RaffleId == id).Select(t => t.Number).ToListAsync();
        }

        public IEnumerable<Ticket> GetRaffleTickets(long id) {
            return _context.Tickets.Where(t => t.RaffleId == id);
        }

        public async Task<IEnumerable<RaffleWinner>> GetRaffleWinners(long id) {
            return await _context.RaffleWinners.Include(rw => rw.UserAccount).Include(rw => rw.Raffle).Include(rw => rw.Prize)
                                               .Where(rw => rw.RaffleId == id).ToListAsync();
        }

        public bool RaffleExists(long id) {
            return _context.Raffles.Any(r => r.Id == id);
        }

        public IEnumerable<Prize> GetRafflePrizes(long id) {
            return _context.Prizes.Where(p => p.RaffleId == id).ToList();
        }
    }
}