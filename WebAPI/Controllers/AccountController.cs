using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.DTOs;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow,
                                 IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;
        }

        // api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDTO loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.userName, loginReq.password);

            APIError apiError = new APIError();
            
            if (user == null)
            {
                apiError.ErrorCode=Unauthorized().StatusCode;
                apiError.ErrorMessage="Invalid user name or password";
                apiError.ErrorDetails="This error appears when provided user name or password does not exist";
                return Unauthorized(apiError);
            }
            
            var loginRes = new LoginResDTO();
            loginRes.userName = user.Username;
            loginRes.token = CreateJWT(user);
            return Ok(loginRes);
        }
        // api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDTO loginReq)
        {
            APIError apiError = new APIError();
            if (loginReq.userName.IsEmpty() || loginReq.password.IsEmpty()) {
                    apiError.ErrorCode = BadRequest().StatusCode;
                    apiError.ErrorMessage = "User name or password can not be blank";
                    return BadRequest(apiError);
            }

            if (await uow.UserRepository.UserAlreadyExists(loginReq.userName)) {
                apiError.ErrorCode=BadRequest().StatusCode;
                apiError.ErrorMessage="User already exists, please try something else";
                return BadRequest(apiError);
            }
            
            uow.UserRepository.Register(loginReq.userName, loginReq.password);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));
            
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);
            
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}