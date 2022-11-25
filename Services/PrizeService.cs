using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.JsonPatch;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class PrizeService : IPrizeService {
        private readonly ApplicationDbContext _context;

        public PrizeService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Prize> CreatePrize(Prize to_create) {
            await _context.AddAsync(to_create);
            await _context.SaveChangesAsync();
            to_create.Raffle = _context.Raffles.Find(to_create.RaffleId);

            return to_create;
        }

        public async Task<IEnumerable<Prize>> GetPrizes() {
            return await _context.Prizes.Include(p => p.Raffle).ToListAsync();
        }

        public async Task<Prize> GetPrize(long id) {
            return await _context.Prizes.Include(p => p.Raffle).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeletePrize(long id) {
            var prize = _context.Prizes.Find(id);

            if(prize == null)
                return false;

            _context.Remove(prize);
            _context.SaveChangesAsync();

            return true;
        }
    }
}