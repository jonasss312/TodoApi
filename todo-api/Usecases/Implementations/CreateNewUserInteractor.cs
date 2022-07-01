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

        public Task CreateNewUser(UserDto userDto, out User newUser)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUser = new User
            {
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Constants.USER_ROLE
            };

            return _userRepo.Insert(newUser);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
