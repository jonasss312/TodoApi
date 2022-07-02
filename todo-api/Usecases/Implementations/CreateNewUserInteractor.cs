using System.Security.Cryptography;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class CreateNewUserInteractor : CreateNewUserUC
    {
        private UserRepo _userRepo;

        public CreateNewUserInteractor(UserRepo userRepo)
        {
            _userRepo = userRepo;
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

            return _userRepo.Insert(newUser);
        }
    }
}
