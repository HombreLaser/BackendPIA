using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics {
    public class CreateAdministratorSessionLogic {
        private readonly ITokenGenerator _token_generator;
        private readonly UserManager<UserAccount> _manager;
        private readonly UserAccountLoginForm _form;
        private AuthenticationToken _token;

        public AuthenticationToken Token { get { return _token; } }

        public CreateAdministratorSessionLogic(ITokenGenerator token_generator, UserManager<UserAccount> manager, UserAccountLoginForm form) {
            _token_generator = token_generator;
            _manager = manager;
            _form = form;
        }

        public async Task<bool> Call() {
            var user = await _manager.FindByEmailAsync(_form.Email);

            if(user == null)
                return false;

            var result = await _manager.CheckPasswordAsync(user, _form.Password);

            if(result) {
                _token = new AuthenticationToken { Token = _token_generator.Generate(user, "administrator"), 
                                                  RefreshToken = _token_generator.GenerateRefreshToken() };
                // We overwrite or set the value of the session token in the database: all other previous logins are invalid.
                user.SessionToken = _token.RefreshToken;
                user.SessionTokenExpiryTime = DateTime.UtcNow.AddHours(3);
                await _manager.UpdateAsync(user);

                return true;
            }

            return false;
        }
    }
}