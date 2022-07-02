using System.Security.Claims;

namespace todo_api.Usecases.Interfaces
{
    public interface VerifyAssignmentCreatorOrAdminUC
    {
        bool VerifyAssignmentCreatorOrAdmin(IEnumerable<Claim> claims, int assignmentUserId);
    }
}
