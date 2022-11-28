using Microsoft.AspNetCore.Identity;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Services {
    public class UserAccountService : IUserAccountService {
        private readonly UserManager<UserAccount> _manager;
        private readonly ApplicationDbContext _context;

        public UserAccountService(UserManager<UserAccount> manager, ApplicationDbContext context) {
            _manager = manager;
            _context = context;
        }

        public async Task<IdentityResult> CreateUserAccount(UserAccount user, string password, string role) {
            var result = await _manager.CreateAsync(user, password);

            if(result.Succeeded)
                await _manager.AddToRoleAsync(user, role);

            return result;
        }

        public async Task<UserAccount> GetUserAccount(string email) {
            var result = await _manager.FindByEmailAsync(email);

            if(result != null)
                result.Tickets = _context.Tickets.Where(t => t.Owner == result).ToList();

            return result;
        }
    }
}