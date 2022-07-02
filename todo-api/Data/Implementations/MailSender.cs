using System.Net;
using System.Net.Mail;
using todo_api.Data.Interfaces;

namespace todo_api.Data.Implementations
{
    public class MailSender : MailGW
    {
        private readonly IConfiguration _configuration;

        public MailSender(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Send(string toMail, string mailBody)
        {
            SmtpClient client = SetupClient();
            try
            {
                client.Send("7874694984264a@gmail.com", toMail, "RECOVERY LINK", mailBody);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private SmtpClient SetupClient()
        {
            return new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("7874694984264a@gmail.com", "uclhsuarornlwyle"),
                EnableSsl = true
            };
        }
    }
}
