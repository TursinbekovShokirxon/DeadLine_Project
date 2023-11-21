using Application.InterfacesModelServices;
using MediatR;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusCreateModel:IRequest<string>
    {
        public string StatusName { get; set; } = string.Empty;

    }
    public class TaskStatusCreateHandler : IRequestHandler<TaskStatusCreateModel, string>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusCreateHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<string> Handle(TaskStatusCreateModel request, CancellationToken cancellationToken)
        {
            var Status = new Domain.Models.TaskStatus() { StatusName = request.StatusName };
            var res=await _service.Create(Status);
            return res!=null?"Статус задания был создан":"Статутс задания не может быть создан";
        }
    }
}
