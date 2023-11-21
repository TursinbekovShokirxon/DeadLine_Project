using Application.InterfacesModelServices;
using MediatR;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusCreateModel:IRequest<bool>
    {
        public string StatusName { get; set; } = string.Empty;

    }
    public class TaskStatusCreateHandler : IRequestHandler<TaskStatusCreateModel, bool>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusCreateHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<bool> Handle(TaskStatusCreateModel request, CancellationToken cancellationToken)
        {
            var Status = new Domain.Models.TaskStatus() { StatusName = request.StatusName };
            var res=await _service.Create(Status);
            return res!=null;
        }
    }
}
