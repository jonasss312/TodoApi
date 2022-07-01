using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class CreateTokenInteractor : CreateTokenUC
    {
        private readonly IConfiguration _configuration;

        public CreateTokenInteractor(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var token = new JwtSecurityToken(
                claims: CreateClaims(user),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: CreateSignature()
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> CreateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
        }

        private SigningCredentials CreateSignature()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
