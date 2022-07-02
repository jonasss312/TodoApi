namespace todo_api.Usecases.Interfaces
{
    public interface SendRecoveryEmailUC
    {
        bool Send(string toMail, string token);
    }
}
