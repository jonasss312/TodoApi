namespace todo_api.Gateway.Interfaces
{
    public interface MailGW
    {
        bool Send(string toMail, string mailBody);
    }
}
