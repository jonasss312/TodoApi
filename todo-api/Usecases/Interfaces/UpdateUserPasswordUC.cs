using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface UpdateUserPasswordUC
    {
        Task UpdateUserPassword(User user, byte[] newPasswordHash, byte[] newPasswordSalt);
    }
}
