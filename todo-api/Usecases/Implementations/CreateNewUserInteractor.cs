using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class CreateNewUserInteractor : CreateNewUserUC
    {
        private UsersGW _usersGW;

        public CreateNewUserInteractor(UsersGW usersGW)
        {
            _usersGW = usersGW;
        }

        public Task CreateNewUser(UserDto userDto, byte[] passwordHash, byte[] passwordSalt, out User newUser)
        {
            newUser = new User
            {
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.USER_ROLE
            };

            return _usersGW.Insert(newUser);
        }
    }
}
