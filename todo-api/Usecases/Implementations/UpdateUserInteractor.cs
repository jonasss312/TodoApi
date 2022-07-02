using todo_api.Gateway.Interfaces;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class UpdateUserInteractor : UpdateUserPasswordUC
    {
        private UsersGW _usersGw;

        public UpdateUserInteractor(UsersGW usersGw)
        {
            _usersGw = usersGw;
        }

        public Task UpdateUserPassword(User user, byte[] newPasswordHash, byte[] newPasswordSalt)
        {
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;

            return _usersGw.Update(user);
        }
    }
}
