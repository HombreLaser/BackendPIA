using System.Security.Claims;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public interface ITokenGenerator {
        public string Generate(UserAccount user, string role);
        public string GenerateRefreshToken();
        public string? GetPrincipalFromToken(string token);
    }
}