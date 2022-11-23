using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Forms;
using BackendPIA.Models;
using BackendPIA.Services;
using Microsoft.AspNetCore.Authorization;
using BackendPIA.Errors;
using BackendPIA.Logics;

namespace BackendPIA.Controllers {
    [Route("api/")]
    [ApiController]
    public class UserAccountsController : ControllerBase {
        private readonly IUserAccountService _user_account_service;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _token_generator;
        private readonly UserManager<UserAccount> _manager;

        public UserAccountsController(UserManager<UserAccount> manager, IUserAccountService user_account_service, 
                                      ITokenGenerator token_generator, IMapper mapper) 
        {
            _user_account_service = user_account_service;
            _mapper = mapper;
            _manager = manager;
            _token_generator = token_generator;
        }

        [HttpGet("user")]
        [Authorize(Roles = "Regular")]
        public async Task<ActionResult<UserAccountDTO>> Show() {
            string email = HttpContext.User.Claims.Where(c => c.Type.Contains("email")).First().Value;
            var user = await _user_account_service.GetUserAccount(email);

            if(user == null)
                return StatusCode(404, "User couldn't be found.");

            return Ok(_mapper.Map<UserAccountDTO>(user));
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthenticationToken>> Create(UserAccountForm form) {
            CreateUserAccountLogic logic = new CreateUserAccountLogic(_token_generator, _manager, form, _mapper, _user_account_service, "Regular");
            var result = await logic.Call();

            if(result) 
                return Ok(logic.Token);

            return StatusCode(422, logic.Errors);
        }
    }
}