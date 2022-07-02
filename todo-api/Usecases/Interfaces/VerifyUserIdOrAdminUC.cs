using System.Security.Claims;

namespace todo_api.Usecases.Interfaces
{
    public interface VerifyUserIdOrAdminUC
    {
        bool VerifyUserIdOrAdmin(IEnumerable<Claim> claims, int userId);
    }
}
