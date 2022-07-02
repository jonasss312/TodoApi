using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using todo_api.Data.Repos;
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

        public UserRegisterController(
            GetUserByEmailUC getUserByEmailUC,
            CreateNewUserUC createNewUserUC)
        {
            _getUserByEmailUC = getUserByEmailUC;
            _createNewUserUC = createNewUserUC; ;
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

            await _createNewUserUC.CreateNewUser(registerUserDto, out User newUser);
            return Ok(newUser);
        }
    }
}
