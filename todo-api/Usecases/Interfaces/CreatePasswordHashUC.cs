namespace todo_api.Usecases.Interfaces
{
    public interface CreatePasswordHashUC
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
