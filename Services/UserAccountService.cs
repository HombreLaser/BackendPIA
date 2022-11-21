using Microsoft.AspNetCore.Identity;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class UserAccountService : IUserAccountService {
        private readonly UserManager<UserAccount> _manager;

        public UserAccountService(UserManager<UserAccount> manager) {
            _manager = manager;
        }

        public async Task<IdentityResult> CreateUserAccount(UserAccount user, string password, string role) {
            var result = await _manager.CreateAsync(user, password);

            if(result.Succeeded)
                await _manager.AddToRoleAsync(user, role);

            return result;
        }
    }
}