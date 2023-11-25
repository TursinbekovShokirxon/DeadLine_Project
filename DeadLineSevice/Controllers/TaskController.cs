using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using Infrastructure.Handlers.ForAuthentication;
using Infrastructure.Handlers.ForTask;
using Infrastructure.Handlers.Task;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskController(IMediator mediator) =>
            _mediator = mediator;
        

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Domain.Models.Task>> TaskCreateAsync(TaskCreateModel request)
        {
            var task = await _mediator.Send(request);
            // Nado napisat logiku gde user prikleplyayetsya k task kak sozdatel
            return Ok(task);
        }


        [HttpGet]
        public async Task<ActionResult<Domain.Models.Task>> TaskGetByIdAsync(TaskGetByIdModel request)
        {
            var task = await _mediator.Send(request);

            if (task == null)
                return BadRequest($"Task с id = {request.Id} не найден");
            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<Domain.Models.Task>> TaskGetAll(TaskGetAllModel request)
        {
            var taskList = _mediator.Send(request);
            
            if(taskList == null)  return BadRequest(taskList);

            return Ok(taskList);
        }

        [HttpPut]
        public async Task<ActionResult<Domain.Models.Task>> TaskUpdateAsync(TaskUpdateModel request)
        {
            var taskList = await _mediator.Send(request);

            if (taskList == false) return BadRequest("Обновление прошло не успешно!");

            return Ok(taskList);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> TaskDeleteAsync(TaskDeleteModel request)
        {
            var taskDelete = await _mediator.Send(request);

            if (!taskDelete) return BadRequest("Удаление не прошло успешно!");

            return Ok(taskDelete);
        }





    }
}
