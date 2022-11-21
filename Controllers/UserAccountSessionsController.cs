using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendPIA.Forms;
using BackendPIA.Models;
using BackendPIA.Services;
using BackendPIA.Errors;
using BackendPIA.Logics;

namespace BackendPIA.Controllers {
    [Route("api/")]
    [ApiController]
    public class AdministratorSessionsController : ControllerBase {
        private readonly ITokenGenerator _token_generator;
        private readonly UserManager<UserAccount> _manager;

        public AdministratorSessionsController(ITokenGenerator token_generator, UserManager<UserAccount> manager) {
            _token_generator = token_generator;
            _manager = manager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationToken>> Create(UserAccountLoginForm form) {
            CreateUserAccountSessionLogic logic = new CreateUserAccountSessionLogic(_token_generator, _manager, form);
            var result = await logic.Call();

            if(result)
                return Ok(logic.Token);

            return StatusCode(401, new InvalidLoginError(401, "Check your credentials"));
        }
    }
}