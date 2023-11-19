using Application;
using MediatR;

namespace Infrastructure.Handlers.Task
{
    public class TaskCreateModel : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Descryption { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public int UserId { get; set; }
    }
    public class PupilCreateHandler : IRequestHandler<TaskCreateModel, string>
    {
        private readonly ITaskService _service;
        public PupilCreateHandler(ITaskService _service) =>
            this._service = _service;

        public async Task<string> Handle(TaskCreateModel request, CancellationToken cancellationToken)
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
    public class TaskCreateHandler
    {
    }
}
