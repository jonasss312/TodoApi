using todo_api.Models;

namespace todo_api.Usecases.Interfaces
{
    public interface CreateTokenUC
    {
        string CreateToken(User user);
    }
}
