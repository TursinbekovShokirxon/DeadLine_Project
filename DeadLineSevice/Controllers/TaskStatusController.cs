using Infrastructure.Handlers.ForTaskStatuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaskStatus = Domain.Models.TaskStatus;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class TaskStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> TaskStatusCreateAsync(TaskStatusCreateModel request)
        {
            var taskStatus = await _mediator.Send(request);
            return taskStatus? "Статус задания был создан" : "Статуc задания не может быть создан";
        }

        [HttpGet]
        public async Task<ActionResult<TaskStatus>> TaskStatusGetByIdAsync(TaskStatusGetByIdModel request)
        {
            var taskStatus = await _mediator.Send(request);

            if (taskStatus == null)
                return BadRequest($"TaskStatus с id = {request.Id} не найден");

            return Ok(taskStatus);
        }

        [HttpGet]
        public async Task<ActionResult<TaskStatus>> TaskStatusGetAll(TaskStatusGetAllModel request)
        {
            var taskStatusList = await _mediator.Send(request);

            if (taskStatusList.Count()==0)
                return BadRequest(taskStatusList);

            return Ok(taskStatusList);
        }

        [HttpPut]
        public async Task<ActionResult<string>> TaskStatusUpdateAsync(TaskStatusUpdateModel request)
        {
            var taskStatusUpdate = await _mediator.Send(request);

            if (!taskStatusUpdate)
                return BadRequest("Обновление провалено!");

            return Ok("Статус обновлен");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> TaskStatusDeleteAsync(TaskStatusDeleteModel request)
        {
            var taskStatusDelete = await _mediator.Send(request);

            if (!taskStatusDelete)
                return BadRequest("Удаление провалено!");

            return Ok("Удаление завершено");
        }
    }
}
