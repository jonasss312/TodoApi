using Microsoft.AspNetCore.Mvc;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly GetUserByEmailUC _getUserByEmailUC;
        private readonly VerifyUserPasswordUC _verifyUserPasswordUC;
        private readonly CreateTokenUC _createTokenUC;

        public UserLoginController(
            GetUserByEmailUC getUserByEmailUC,
            VerifyUserPasswordUC verifyUserPasswordUC,
             CreateTokenUC createTokenUC)
        {
            _getUserByEmailUC = getUserByEmailUC;
            _verifyUserPasswordUC = verifyUserPasswordUC;
            _createTokenUC = createTokenUC;
        }

        [HttpPost]
        [Route(Constants.LOGIN_COMMAND)]
        public async Task<ActionResult<string>> Login(UserDto loginDto)
        {
            var user = await _getUserByEmailUC.GetUserByEmail(loginDto.Email);
            if(user == null)
                return BadRequest(Constants.ERROR_INVALID_LOGIN_DETAILS);

            if(!_verifyUserPasswordUC.VerifyUserPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest(Constants.ERROR_INVALID_LOGIN_DETAILS);

            return Ok(_createTokenUC.CreateToken(user));
        }
    }
}
