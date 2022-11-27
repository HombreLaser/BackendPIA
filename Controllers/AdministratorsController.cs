using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Forms;
using BackendPIA.Models;
using BackendPIA.Services;
using BackendPIA.Logics;

namespace BackendPIA.Controllers {
    [Route("api/admin")]
    [ApiController]
    public class AdministratorsController : ControllerBase {
        private readonly IUserAccountService _user_account_service;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _token_generator;
        private readonly UserManager<UserAccount> _manager;

        public AdministratorsController(UserManager<UserAccount> manager, IUserAccountService user_account_service, 
                                        ITokenGenerator token_generator, IMapper mapper) 
        {
            _user_account_service = user_account_service;
            _mapper = mapper;
            _token_generator = token_generator;
            _manager = manager;
        }

        [Authorize(Roles = "Administrator", Policy = "ValidToken")]
        [HttpPost("signup")]
        public async Task<ActionResult<AuthenticationToken>> Create(UserAccountForm form) {
            CreateUserAccountLogic logic = new CreateUserAccountLogic(_token_generator, _manager, form, _mapper, _user_account_service, "Administrator");
            var result = await logic.Call();

            if(result) 
                return Ok(logic.Token);

            return StatusCode(422, logic.Errors);
        }
    }
}