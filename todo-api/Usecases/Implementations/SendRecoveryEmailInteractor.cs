using System.Net;
using System.Net.Mail;
using todo_api.Data.Interfaces;
using todo_api.Usecases.Interfaces;

namespace todo_api.Usecases.Implementations
{
    public class SendRecoveryEmailInteractor : SendRecoveryEmailUC
    {
        private readonly MailGW _mailGW;
        public SendRecoveryEmailInteractor(MailGW mailGW)
        {
            _mailGW = mailGW;
        }

        public bool Send()
        {
            return _mailGW.Send("7874694984264a@gmail.com", "TEST");
        }
    }
}
