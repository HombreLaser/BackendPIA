using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Forms;
using BackendPIA.Models;

namespace BackendPIA.Logics {
    public abstract class BaseUserAccountLogic {
        protected readonly ITokenGenerator _token_generator;
        protected readonly UserManager<UserAccount> _manager;
        protected AuthenticationToken? _token;
        public AuthenticationToken? Token { get { return _token; } }

        public BaseUserAccountLogic(ITokenGenerator token_generator, UserManager<UserAccount> manager) {
            _manager = manager;
            _token_generator = token_generator;
        }

        protected async Task SetAuthenticationToken(UserAccount user) {
            _token = new AuthenticationToken { Token = _token_generator.Generate(user, "administrator"), 
                                               RefreshToken = _token_generator.GenerateRefreshToken() };
            await SetUserRefreshToken(user);
        }

        // We overwrite or set the value of the session token in the database: all other previous logins are invalid.
        private async Task SetUserRefreshToken(UserAccount user) {
            user.SessionToken = _token.RefreshToken;
            user.SessionTokenExpiryTime = DateTime.UtcNow.AddHours(3);
            await _manager.UpdateAsync(user);
        }
    }
}