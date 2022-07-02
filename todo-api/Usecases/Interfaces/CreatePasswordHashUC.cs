using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Usecases.Interfaces
{
    public interface CreatePasswordHashUC
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
