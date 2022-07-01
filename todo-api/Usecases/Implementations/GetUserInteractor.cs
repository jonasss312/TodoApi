using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class GetUserInteractor : GetUserByEmailUC
    {
        private UserRepo _userRepo;

        public GetUserInteractor(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<User> GetUserByEmail(string email)
        { 
            return _userRepo.GetUserByEmail(email);
        }
    }
}
