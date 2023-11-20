using Application.ModelServices;
using MediatR;

namespace Infrastructure.Handlers.ForTask
{
    public class TaskCreateModel : IRequest<Domain.Models.Task>
    {
        public string Name { get; set; } = string.Empty;
        public string Descryption { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public int UserId { get; set; }
    }
    public class TaskCreateHandler : IRequestHandler<TaskCreateModel, Domain.Models.Task>
    {
        private readonly ITaskService _service;
        public TaskCreateHandler(ITaskService _service) =>
            this._service = _service;

        public async Task<Domain.Models.Task> Handle(TaskCreateModel request, CancellationToken cancellationToken)
        {
            Domain.Models.Task task = new();
            task.Descryption = request.Descryption;
            task.Deadline = request.Deadline;
            task.UserId = request.UserId;
            task.Name = request.Name;
            var entity = await _service.Create(task);
            return entity;
        }
    }
}
