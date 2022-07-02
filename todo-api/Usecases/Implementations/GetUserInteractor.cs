using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetUserInteractor : GetUserByEmailUC, GetUserByTokenUC
    {
        private UsersGW _usersGw;

        public GetUserInteractor(UsersGW usersGw)
        {
            _usersGw = usersGw;
        }

        public Task<User> GetUserByEmail(string email)
        {
            return _usersGw.GetUserByEmail(email);
        }

        public Task<User> GetUserByToken(string token)
        {
            try
            {
                int userId = int.Parse(
                    new JwtSecurityTokenHandler().
                    ReadJwtToken(token)
                    .Claims.
                    FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier))
                    .Value);
                return _usersGw.GetUserById(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
