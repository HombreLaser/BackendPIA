using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using BackendPIA.Models;

namespace BackendPIA.Services {
    public class TokenGenerator : ITokenGenerator {
        private readonly string _key;
        public TokenGenerator(string key) {
            _key = key;
        }

        public string Generate(UserAccount user, string role) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
	        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(1);
            //var issuer = _configuration["Jwt:Issuer"];
            var claims = new List<Claim> {
		        new Claim("sid", user.Id),
		        new Claim("username", user.UserName),
		        new Claim("email", user.Email),
                new Claim("role", role)
	        };
            var descriptor = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(descriptor);
        }

        public string GenerateRefreshToken() {
            byte[] random_number = new byte[16];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(random_number);
            
            return Convert.ToBase64String(random_number);
        }
    }
}