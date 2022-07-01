using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface VerifyUserPasswordUC
    {
        bool VerifyUserPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
