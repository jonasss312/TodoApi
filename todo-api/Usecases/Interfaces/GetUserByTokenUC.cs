using System.Security.Claims;
using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface GetUserByTokenUC
    {
        Task<User> GetUserByToken(string token);
    }
}
