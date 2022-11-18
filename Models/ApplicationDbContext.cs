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
            string user_id = "24edc3d6-bf9c-41a1-9371-224e4419ccb0";
            string role_id = "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec";
            var hasher = new PasswordHasher<UserAccount>();
            UserAccount user_seed = new UserAccount { Id = user_id, UserName = "admin", Email = "admin@example.com",
                                                      NormalizedEmail = "ADMIN@EXAMPLE.COM", NormalizedUserName = "ADMIN" };
            // TODO: save the seeded admin password in a user secret.
            user_seed.PasswordHash = hasher.HashPassword(user_seed, "admin_password");
            builder.Entity<UserAccount>().HasData(user_seed);
            builder.Entity<IdentityRole>().HasData(new IdentityRole {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = role_id,
                ConcurrencyStamp = role_id
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = user_id, RoleId = role_id });
        }
    }
}
