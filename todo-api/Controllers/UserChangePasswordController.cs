using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH)]
    [ApiController]
    public class UserChangePasswordController : ControllerBase
    {
        private readonly GetUserByEmailUC _getUserByEmailUC;
        private readonly SendRecoveryEmailUC _sendRecoveryEmailUC;
        private readonly CreateTokenUC _createTokenUC;
        private readonly GetUserByTokenUC _getUserByTokenUC;
        private readonly UpdateUserPasswordUC _updateUserPasswordUC;
        private readonly CreatePasswordHashUC _createPasswordHashUC;

        public UserChangePasswordController(
             GetUserByEmailUC getUserByEmailUC,
             GetUserByTokenUC getUserByTokenUC,
             SendRecoveryEmailUC sendRecoveryEmailUC,
             CreateTokenUC createTokenUC,
             UpdateUserPasswordUC updateUserPasswordUC,
             CreatePasswordHashUC createPasswordHashUC)
        {
            _getUserByEmailUC = getUserByEmailUC;
            _sendRecoveryEmailUC = sendRecoveryEmailUC;
            _createTokenUC = createTokenUC;
            _getUserByTokenUC = getUserByTokenUC;
            _updateUserPasswordUC = updateUserPasswordUC;
            _createPasswordHashUC = createPasswordHashUC;
        }

        [HttpPost]
        [Route(Constants.SEND_RESET_PASSWORD_MAIL)]
        public async Task<ActionResult<string>> RecoverPassword(UserRecoverDto recoverDto)
        {
            var user = await _getUserByEmailUC.GetUserByEmail(recoverDto.Email);

            if(!new EmailAddressAttribute().IsValid(recoverDto.Email))
                return BadRequest(Constants.ERROR_INVALID_EMAIL);
            if(user == null)
                return BadRequest(Constants.ERROR_USER_DOES_NOT_EXISTS);
            if(!_sendRecoveryEmailUC.Send(recoverDto.Email, _createTokenUC.CreateToken(user)))
                return StatusCode(StatusCodes.Status502BadGateway, Constants.ERROR_SENDING_EMAIL);

            return Ok(Constants.SUCCESS_EMAIL_SENT);
        }

        [HttpGet]
        [Route(Constants.CHANGE_PASSWORD_COMMAND + "/" + Constants.TOKEN + "/" + Constants.PASSWORD)]
        public async Task<ActionResult<string>> ChangePassword(string token, string password)
        {
            var user = await _getUserByTokenUC.GetUserByToken(token);

            if(user == null)
                return BadRequest(Constants.ERROR_USER_DOES_NOT_EXISTS);
            if(password.Length < 12)
                return BadRequest(Constants.ERROR_PASSWORD_TOO_SHORT);

            _createPasswordHashUC.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            await _updateUserPasswordUC.UpdateUserPassword(user, passwordHash, passwordSalt);

            return Ok(Constants.SUCCESS_PASSWORD_CAHNGED);
        }
    }
}
