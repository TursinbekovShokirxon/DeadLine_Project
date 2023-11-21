using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using Infrastructure.Handlers.ForAuthentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Cryptography;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    //[EnableRateLimiting("BucketWindowPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenServices _tokenServices;
        public AuthController(IMediator mediator,ITokenServices tokenServices)
        {
            _mediator = mediator;
            _tokenServices = tokenServices;
        }
        [HttpPost]
        public async Task<ActionResult<UserAuth>> Registration(UserRegirstrationModel request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(UserLoginModel request)
        {
            string token;
            UserAuth res = await _mediator.Send(request);

            if (res != null)
            {
                if (_tokenServices.VerifyPassword(request.Password,res.PasswordHash))
                {
                    token = _tokenServices.GenerateToken(res);
                    var refreshToken = _tokenServices.GenerateRefreshToken();

                    SetRefreshToken(res, refreshToken);

                    return Ok(token);
                }
                else return BadRequest("Неправильный пароль");
            }
                return BadRequest($"Пользователь под именем {request.Username} не найден");
        }

        //[HttpPost("Refresh-Token")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];

        //    //if()
        //}
        private void SetRefreshToken(UserAuth user,RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expired,

            };
            
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expired;

        }

    }
}
