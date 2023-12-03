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
        public async Task<ActionResult<string>> CreateRole([FromBody] CreateRoleModel role)
        {
            var result = await _mediator.Send(role);
            return result != null ? Ok("Роль создана") : BadRequest("Ошибка создания роли");
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateRole([FromBody] UpdateRoleModel obj)
        {
            var result = await _mediator.Send(obj);
            return result != null ? Ok("Роль обновлена") : BadRequest("Ошибка обновления роли");
        }

        [HttpGet("api/Role/GetAllRole")]
        public async Task<IEnumerable<Role>> GetAllRole()
        {
            GetAllRoleModel obj = new();
            var result = await _mediator.Send(obj);
            return result;
        }
        [HttpPost("api/Role/AddPermissionInRole")]
        public async Task<ActionResult<string>> AddPermissionInRoleHandler([FromBody] AddPermissionInRoleHandler obj)
        {
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
        [HttpPost("api/Role/AddUserInRole")]
        public async Task<ActionResult<string>> AddUserInRoleHandler([FromBody] AddUserInRoleHandler obj)
        {
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteRole(DeleteRoleModel obj)
        {
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
    }
}