using Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Task
{
    public class TaskGetByIdModel : IRequest<Domain.Task>
    {
        public int Id { get; set; }
    }
    public class TaskGetByIdHandler : IRequestHandler<TaskGetByIdModel, Domain.Task>
    {
        private readonly ITaskService _service;
        public TaskGetByIdHandler(ITaskService _service)
        {
            this._service = _service;
        }
        public async Task<Domain.Task> Handle(TaskGetByIdModel request, CancellationToken cancellationToken)
        {
            var task = await _service.GetById(request.Id);
            return task;
        }
    }
}
