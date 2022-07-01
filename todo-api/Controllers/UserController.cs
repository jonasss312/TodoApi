using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using todo_api.Data.Repos;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Controllers
{
    [Route(Constants.API_PATH + Constants.USERS_PATH)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepo _userRepo;
        private IConfiguration _configuration;

        public UserController(IConfiguration configuration, UserRepo userRepo)
        {
            _configuration = configuration;
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto registerUserDto)
        {
            var user = await _userRepo.GetUserByEmail(registerUserDto.Email);

            if(user != null)
                return BadRequest(Constants.ERROR_CANNOT_CREATE_NEW_USER);
            if(!new EmailAddressAttribute().IsValid(registerUserDto.Email))
                return BadRequest(Constants.ERROR_INVALID_EMAIL);

            User newUser = CreateNewUser(registerUserDto);
            await _userRepo.Insert(newUser);
            return Ok(newUser);
        }

        private User CreateNewUser(UserDto registerUserDto)
        {
            CreatePasswordHash(registerUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            return new User
            {
                Email = registerUserDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserDto loginDto)
        {
            var user = await _userRepo.GetUserByEmail(loginDto.Email);

            if(user == null)
                return BadRequest(Constants.ERROR_INVALID_LOGIN_DETAILS);

            if(!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest(Constants.ERROR_INVALID_LOGIN_DETAILS);

            return Ok(CreateToken(user));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        // TODO invalid token on web, secret key problem?
        private string CreateToken(User user)
        {
            var token = new JwtSecurityToken(
                claims: CreateClaims(user),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: CreateSignature()
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> CreateClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
        }

        private SigningCredentials CreateSignature()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
