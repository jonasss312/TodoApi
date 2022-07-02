using System.Net;
using System.Net.Mail;
using todo_api.Gateway.Interfaces;

namespace todo_api.Gateway.Implementations
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
            try
            {
                SmtpClient client = SetupClient();
                client.Send(_configuration.GetSection("MailFromAdress").Value, toMail, "RECOVERY LINK", mailBody);
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
            return new SmtpClient(_configuration.GetSection("MailHost").Value, int.Parse(_configuration.GetSection("MailPort").Value))
            {
                Credentials = new NetworkCredential(_configuration.GetSection("MailFromAdress").Value, _configuration.GetSection("MailPassword").Value),
                EnableSsl = true
            };
        }
    }
}
