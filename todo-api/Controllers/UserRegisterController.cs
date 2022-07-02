using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Usecases.Interfaces;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH)]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly GetUserByEmailUC _getUserByEmailUC;
        private readonly CreateNewUserUC _createNewUserUC;
        private readonly CreatePasswordHashUC _createPasswordHashUC;

        public UserRegisterController(
            GetUserByEmailUC getUserByEmailUC,
            CreateNewUserUC createNewUserUC,
            CreatePasswordHashUC createPasswordHashUC)
        {
            _getUserByEmailUC = getUserByEmailUC;
            _createNewUserUC = createNewUserUC; ;
            _createPasswordHashUC = createPasswordHashUC;
        }

        [HttpPost]
        [Route(Constants.REGISTER_COMMAND)]
        public async Task<ActionResult<User>> Register(UserDto registerUserDto)
        {
            var user = await _getUserByEmailUC.GetUserByEmail(registerUserDto.Email);

            if(registerUserDto.Password.Length < 12)
                return BadRequest(Constants.ERROR_PASSWORD_TOO_SHORT);
            if(!new EmailAddressAttribute().IsValid(registerUserDto.Email))
                return BadRequest(Constants.ERROR_INVALID_EMAIL);
            _createPasswordHashUC.CreatePasswordHash(registerUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            await _createNewUserUC.CreateNewUser(registerUserDto, passwordHash, passwordSalt, out User newUser);
            return Ok(newUser);
        }
    }
}
