using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserChangePasswordController : ControllerBase
    {
        private readonly GetUserByEmailUC _getUserByEmailUC;
        private readonly SendRecoveryEmailUC _sendRecoveryEmailUC;

        public UserChangePasswordController(
            GetUserByEmailUC getUserByEmailUC,
             SendRecoveryEmailUC sendRecoveryEmailUC)
        {
            _getUserByEmailUC = getUserByEmailUC;
            _sendRecoveryEmailUC = sendRecoveryEmailUC;
        }

        [HttpPost]
        [Route(Constants.RESET_PASSWORD_COMMAND)]
        public async Task<ActionResult<string>> RecoverPassword(UserRecoverDto recoverDto)
        {
            var user = await _getUserByEmailUC.GetUserByEmail(recoverDto.Email);

            if(!new EmailAddressAttribute().IsValid(recoverDto.Email))
                return BadRequest(Constants.ERROR_INVALID_EMAIL);
            if(user == null)
                return BadRequest(Constants.ERROR_USER_DOES_NOT_EXISTS);
            if(!_sendRecoveryEmailUC.Send())
                return StatusCode(StatusCodes.Status502BadGateway, Constants.ERROR_SENDING_EMAIL);

            return Ok(Constants.SUCCESS_EMAIL_SENT);
        }
    }
}
