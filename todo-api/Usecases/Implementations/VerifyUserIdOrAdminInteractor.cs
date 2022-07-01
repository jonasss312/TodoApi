using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class VerifyUserIdOrAdminInteractor : VerifyUserIdOrAdminUC
    {
        public bool VerifyUserIdOrAdmin(IEnumerable<Claim> claims, int userId)
        {
            if(hasAdminRole(claims))
                return true;
            return isSameUser(claims, userId);
        }

        private bool hasAdminRole(IEnumerable<Claim> claims)
        {
            return claims.Any(claim => claim.Type.Equals(ClaimTypes.Role) && claim.Value == Constants.ADMIN_ROLE);
        }

        private bool isSameUser(IEnumerable<Claim> claims, int userId)
        {
            return claims.Any(claim => claim.Type.Equals(ClaimTypes.NameIdentifier) && claim.Value == userId.ToString());
        }
    }
}
