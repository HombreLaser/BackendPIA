using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ApplicationDbContext _context;
        private readonly ITokenGenerator _token_generator;
        private readonly UserManager<UserAccount> _manager;

        public AdministratorSessionsController(ITokenGenerator token_generator, UserManager<UserAccount> manager, ApplicationDbContext context) {
            _token_generator = token_generator;
            _manager = manager;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationToken>> Create(UserAccountLoginForm form) {
            CreateUserAccountSessionLogic logic = new CreateUserAccountSessionLogic(_token_generator, _manager, form);
            var result = await logic.Call();

            if(result)
                return Ok(logic.Token);

            return StatusCode(401, new InvalidLoginError(401, "Check your credentials"));
        }

        [Authorize(Policy = "ValidToken")]
        [HttpDelete("logout")]
        public async Task<ActionResult> Delete() {
            string email = HttpContext.User.Claims.Where(c => c.Type.Contains("email")).First().Value;
            DestroyUserAccountSessionLogic logic = new DestroyUserAccountSessionLogic(_manager, email);
            bool result = await logic.Call();

            if(result)
                return Ok();

            return NotFound(new NotFoundError(404, "Couldn't find the user."));
        }

        // [Authorize]
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationToken>> Refresh(AuthenticationToken form) {
            RefreshTokenLogic logic = new RefreshTokenLogic(_token_generator, _manager, form);
            bool result = await logic.Call();

            if(result)
                return Ok(logic.Token);

            return StatusCode(403, new ExpiredSessionError(401, "Check your refresh token"));
        }    
    }
}