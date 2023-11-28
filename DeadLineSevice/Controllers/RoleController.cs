using Domain.Models.Authtification;
using Infrastructure.Handlers.ForRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("api/Role/CreateRole")]
        public async Task<ActionResult<string>> CreateRole([FromBody] CreateRoleModel role)
        {
            var result = await _mediator.Send(role);
            return result != null ? Ok("Роль создана") : BadRequest("Ошибка создания роли");
        }

        [HttpPut("api/Role/UpdateRole")]
        public async Task<ActionResult<string>> UpdateRole([FromBody] UpdateRoleHandler obj)
        {
            var result = await _mediator.Send(obj);
            return result != null ? Ok("Роль обновлена") : BadRequest("Ошибка обновления роли");
        }

        [HttpGet("api/Role/GetAllRole")]
        public async Task<IEnumerable<Role>> GetAllRole(GetAllRoleModel obj)
        {
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
        [HttpDelete("api/Role/DeleteRole")]
        public async Task<ActionResult<string>> DeleteRole(DeleteRoleHandler obj)
        {
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
    }
}