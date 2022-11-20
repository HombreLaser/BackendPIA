using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BackendPIA.Services;
using BackendPIA.Models;
using BackendPIA.Forms;

namespace BackendPIA.Logics{
    public class CreateAdministratorLogic : BaseUserAccountLogic {
        private readonly UserAccountForm _form;
        private readonly IMapper _mapper;
        private readonly IUserAccountService _user_account_service;
        private IEnumerable<IdentityError> _errors;
        public IEnumerable<IdentityError> Errors { get => _errors; }

        public CreateAdministratorLogic(ITokenGenerator token_generator, UserManager<UserAccount> manager, UserAccountForm form, 
                                        IMapper mapper, IUserAccountService service) : base(token_generator, manager) 
        {
            _form = form;
            _mapper = mapper;
            _user_account_service = service;
        }

        public async Task<bool> Call() {
            UserAccount user = _mapper.Map<UserAccount>(_form);
            var result = await _user_account_service.CreateUserAccount(user, _form.Password, "Administrator");

            if(result.Succeeded) {
                SetAuthenticationToken(user);

                return true;
            }

            _errors = result.Errors;
            return false;
        }
    }
}