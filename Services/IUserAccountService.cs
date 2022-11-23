using Microsoft.AspNetCore.Identity;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface IUserAccountService {
        public Task<IdentityResult> CreateUserAccount(UserAccount user, string password, string role);
        public Task<UserAccount> GetUserAccount(string email);
    }
}