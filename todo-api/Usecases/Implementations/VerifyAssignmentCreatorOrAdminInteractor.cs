using System.Security.Claims;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class VerifyAssignmentCreatorOrAdminInteractor : VerifyAssignmentCreatorOrAdminUC
    {
        public bool VerifyAssignmentCreatorOrAdmin(IEnumerable<Claim> claims, int assignmentUserId)
        {
            if(hasAdminRole(claims))
                return true;
            return isCreator(claims, assignmentUserId);
        }

        private bool hasAdminRole(IEnumerable<Claim> claims)
        {
            return claims.Any(claim => claim.Type.Equals(ClaimTypes.Role) && claim.Value == Constants.ADMIN_ROLE);
        }

        private bool isCreator(IEnumerable<Claim> claims, int assignmentUserId)
        {
            return claims.Any(claim => claim.Type.Equals(ClaimTypes.NameIdentifier) && claim.Value == assignmentUserId.ToString());
        }
    }
}
