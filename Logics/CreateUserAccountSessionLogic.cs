using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics {
    public class CreateUserAccountSessionLogic : BaseUserAccountLogic {
        private readonly UserAccountLoginForm _form;

        public CreateUserAccountSessionLogic(ITokenGenerator token_generator, UserManager<UserAccount> manager, UserAccountLoginForm form) : base(token_generator, manager) {
            _form = form;
        }

        public async Task<bool> Call() {
            var user = await _manager.FindByEmailAsync(_form.Email);

            if(user == null )
                return false;
                
            var result = await _manager.CheckPasswordAsync(user, _form.Password);

            if(result) {
                await SetAuthenticationToken(user);
                await SetUserRefreshToken(user);

                return true;
            }

            return false;
        }
    }
}