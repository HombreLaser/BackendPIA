using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendPIA.Models {
    public class ApplicationDbContext : IdentityDbContext<UserAccount> {
	    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            Guid user_id = Guid.NewGuid();
            Guid role_id = Guid.NewGuid();
            var hasher = new PasswordHasher<UserAccount>();
            UserAccount user_seed = new UserAccount { Id = user_id.ToString(), UserName = "admin", Email = "admin@example.com",
                                                      NormalizedEmail = "ADMIN@EXAMPLE.COM", NormalizedUserName = "ADMIN" };
            // TODO: save the seeded admin password in a user secret.
            user_seed.PasswordHash = hasher.HashPassword(user_seed, "admin_password");
            builder.Entity<UserAccount>().HasData(user_seed);
            builder.Entity<IdentityRole>().HasData(new IdentityRole {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = role_id.ToString(),
                ConcurrencyStamp = role_id.ToString()
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = user_id.ToString(), RoleId = role_id.ToString() });
        }
    }
}
