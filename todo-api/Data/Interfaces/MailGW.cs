namespace todo_api.Data.Interfaces
{
    public interface MailGW
    {
        bool Send(string toMail, string mailBody);
    }
}
