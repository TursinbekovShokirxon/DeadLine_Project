using Domain.Models;
using Infrastructure.Handlers.ForUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class UserController:ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
        [HttpPost]
        public async Task<User> CreateUser(UserCreateModel user)
        {
            var res = await _mediator.Send(user);
            return res;
        }
    }
}
