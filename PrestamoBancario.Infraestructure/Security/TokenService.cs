using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrestamoBancario.Domain.Constracts.Security;
using PrestamoBancario.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrestamoBancario.Infraestructure.Security
{
    internal class TokenService : ITokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IConfiguration cfg)
        {
            _key = cfg["Jwt:Key"]!;
            _issuer = cfg["Jwt:Issuer"]!;
            _audience = cfg["Jwt:Audience"]!;
        }

        public string CreateToken(Usuario user)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new("Email", user.Email),
            new("Userid", user.Id.ToString()),
            new(ClaimTypes.Role, user.Rol)
        };
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_issuer, _audience, claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
