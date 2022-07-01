using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Usecases.Interfaces
{
    public interface CreateNewUserUC
    {
        Task CreateNewUser(UserDto userDto, out User newUser);
    }
}
