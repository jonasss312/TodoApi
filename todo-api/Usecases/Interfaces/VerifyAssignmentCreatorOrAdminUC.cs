using System.Security.Claims;
using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface VerifyAssignmentCreatorOrAdminUC
    {
        bool VerifyAssignmentCreatorOrAdmin(IEnumerable<Claim> claims, int assignmentUserId);
    }
}
