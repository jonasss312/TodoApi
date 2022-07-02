using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetUserInteractor : GetUserByEmailUC, GetUserByTokenUC
    {
        private UserRepo _userRepo;

        public GetUserInteractor(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<User> GetUserByEmail(string email)
        {
            return _userRepo.GetUserByEmail(email);
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
                return _userRepo.GetUserById(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
