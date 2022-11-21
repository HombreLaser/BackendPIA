using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics {
    public class RefreshTokenLogic : BaseUserAccountLogic {
        private readonly AuthenticationToken _form;

        public RefreshTokenLogic(ITokenGenerator token_generator, UserManager<UserAccount> manager, AuthenticationToken form) : base(token_generator, manager) {
            _form = form;
        }

        public async Task<bool> Call() {
            var email = _token_generator.GetPrincipalFromToken(_form.Token);

            if(email == null)
                return false;
            // Checks.
            var user = await _manager.FindByEmailAsync(email);

            if(user == null)
                return false;

            if(user.SessionTokenExpiryTime == null || user.SessionTokenExpiryTime < DateTime.UtcNow 
               || user.SessionToken == null || user.SessionToken != _form.RefreshToken) {
                user.SessionToken = null;
                user.SessionTokenExpiryTime = null;
                _manager.UpdateAsync(user);

                return false;
            }
            
            await SetAuthenticationToken(user);
            _token.RefreshToken = user.SessionToken;

            return true;
        }
    }
}