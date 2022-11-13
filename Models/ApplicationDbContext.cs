using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendPIA.Models {
    public class ApplicationDbContext : IdentityDbContext {
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
	}
    }
}
