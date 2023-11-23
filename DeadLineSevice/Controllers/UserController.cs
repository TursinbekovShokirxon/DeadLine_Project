using Domain.Models;
using Infrastructure.Handlers.ForUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.TeamFoundation.Core.WebApi;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    [Authorize]
    public class UserController:ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator _mediator)=>
            this._mediator = _mediator;
        

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateModel user)
        {
            var res = await _mediator.Send(user);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateUser(UserUpdateModel user)
        {
            var res = await _mediator.Send(user);
            return Ok(res);
        }


        [HttpGet]
        public async Task<ActionResult<User>> GetByIdUser(UserGetByIdModel user)
        {

            var res = await _mediator.Send(user);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUser(UserGetAllModel user)
        {
            var res = await _mediator.Send(user);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser([FromForm]UserDeleteModel user)
        {
            var res = await _mediator.Send(user);
            return Ok(res);
        }
    }
}
