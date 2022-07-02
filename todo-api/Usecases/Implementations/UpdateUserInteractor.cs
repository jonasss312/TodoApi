using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class UpdateUserInteractor : UpdateUserPasswordUC
    {
        private UserRepo _userRepo;

        public UpdateUserInteractor(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Task UpdateUserPassword(User user, byte[] newPasswordHash, byte[] newPasswordSalt)
        {
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;

            return _userRepo.Update(user);
        }
    }
}
