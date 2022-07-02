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

        public bool Send(string toMail, string token)
        {
            string changePasswordPath = Constants.SERVER_URL + Constants.API_PATH + Constants.USERS_PATH +
                Constants.CHANGE_PASSWORD_COMMAND + "/" + token + "/YourNewPasswordHere";

            return _mailGW.Send(toMail, changePasswordPath);
        }
    }
}
