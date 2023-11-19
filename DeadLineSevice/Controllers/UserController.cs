using Infrastructure.Handlers.ForUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("[controller]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class UserController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
        [HttpPost]
        public async Task<string> CreateUser(UserCreateModel user)
        {
            var res = await _mediator.Send(user);
            return res;
        }
    }
}
