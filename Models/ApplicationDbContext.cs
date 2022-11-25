using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendPIA.Models {
    public class ApplicationDbContext : IdentityDbContext<UserAccount> {
        public DbSet<Raffle>? Raffles { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<Prize>? Prizes { get; set; }
        public DbSet<RaffleWinner>? RaffleWinners { get; set; }

	    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
    }
}
