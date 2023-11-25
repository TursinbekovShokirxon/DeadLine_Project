using Domain.Models.Authtification;
using Infrastructure.Handlers.ForPermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeadLineService.Controllers
{
    [ApiController,Route("api/[controller]/[action]")]
    public class PermissionController:ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async  Task<IEnumerable<Permission>> GetAll(PerMissionGetAllModel obj) {
        var res= await _mediator.Send(obj);
            return res;
        }
    }
}
