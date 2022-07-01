using System.Security.Claims;
using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface VerifyUserIdOrAdminUC
    {
        bool VerifyUserIdOrAdmin(IEnumerable<Claim> claims, int userId);
    }
}
