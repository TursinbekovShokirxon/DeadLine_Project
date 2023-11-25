using Domain.Models.Authtification;
using Infrastructure.Handlers.ForRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateRole(CreateRoleModel role)
        {
            var result = _mediator.Send(role);
            return result != null ? Ok("Роль создана") : BadRequest("Ошибка создания роли");
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateRole(UpdateRoleHandler obj)
        {
            var result = _mediator.Send(obj);
            return result != null ? Ok("Роль обновлена") : BadRequest("Ошибка обновления роли");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRole(GetAllRoleModel obj)
        {
            var result = _mediator.Send(obj);
            return Ok(result);
        }

    }
}