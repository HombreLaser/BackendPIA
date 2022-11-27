using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics {
    public class DestroyUserAccountSessionLogic {
        private readonly UserManager<UserAccount> _manager;
        private readonly string _email;

        public DestroyUserAccountSessionLogic(UserManager<UserAccount> manager, string email) {
            _manager = manager;
            _email = email;
        }   

        public async Task<bool> Call() {
            var user = await _manager.FindByEmailAsync(_email);

            if(user == null)
                return false;

            user.SessionToken = null;
            user.CurrentToken = null;
            user.SessionTokenExpiryTime = null;
            await _manager.UpdateAsync(user);

            return true;
        }
    }
}