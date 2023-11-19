using Domain.Models.Authtification;
using Infrastructure.Handlers.ForAuthentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace DeadLineService.Controllers
{
    [ApiController,Route("api/[controller]/[action]")]
    public class AuthController:ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<UserAuth>> Registration(UserAuthenticationModel reques)
        {
            UserAuth user = new();
            CreatePasswordHash(reques.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            user.Username= reques.Username;
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            
            return Ok(user);
        }
        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserAuth>> Login(UserAuthenticationModel request)
        {
            if (user.Username != request.Username) return BadRequest("user not found");
            return Ok("My crazy token");
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken {
                Token=Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired=DateTime.Now.AddDays(7),
            };
            return refreshToken;
        }
        //private void SetrefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expired,
        //    };
        //    Response.Cookies.Add(cookieOptions);
        //}
    }
}
