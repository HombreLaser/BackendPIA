using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Authorization;
using BackendPIA.Models;

namespace BackendPIA.Policies {
    public class CorrectTokenHandler : AuthorizationHandler<CorrectTokenRequirement> {
        private readonly UserManager<UserAccount> _manager;

        public CorrectTokenHandler(UserManager<UserAccount> manager) {
            _manager = manager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CorrectTokenRequirement requirement) {
            if(context.Resource is HttpContext httpContext) {
                var user = _manager.FindByEmailAsync(context.User.Claims.Where(c => c.Type.Contains("email")).First().Value).Result;

                if(user != null) {
                    string token = httpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

                    if(user.CurrentToken != null && user.CurrentToken == token)
                        context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}