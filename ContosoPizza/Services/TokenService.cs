using ContosoPizza.Models;
using ContosoPizza.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContosoPizza.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, List<Claims> userClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication").GetValue<string>("JWTKey").ToString());

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (var claim in userClaims)
            {
                claims.Add(new Claim("Claim", claim.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
