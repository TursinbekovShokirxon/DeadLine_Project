using Domain.Models.Authtification;
using Infrastructure.Handlers.ForAuthentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
        [HttpPost]
        public async Task<ActionResult<UserAuth>> Registration(UserRegirstrationModel request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserAuth>> Login(UserLoginModel request)
        {
            UserAuth res = await _mediator.Send(request);
            if (res == null) return BadRequest($"Пользователь под именем {request.Username} не найден");
            return Ok(res);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired = DateTime.Now.AddDays(7),
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
