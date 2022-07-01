using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface GetUserByEmailUC
    {
        Task<User> GetUserByEmail(string email);
    }
}
