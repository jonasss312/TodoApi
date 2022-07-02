using todo_api.Models;

namespace todo_api.Gateway.Interfaces
{
    public interface UsersGW
    {
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById(int userId);

        Task Update(User user);

        Task Insert(User user);
    }
}
