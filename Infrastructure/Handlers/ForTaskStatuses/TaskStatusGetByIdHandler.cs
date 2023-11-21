using Application.InterfacesModelServices;
using Domain.Models;
using Infrastructure.Handlers.Task;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusGetByIdModel:IRequest<Domain.Models.TaskStatus>
    {
        public int Id { get; set; }
    }

    public class TaskStatusGetByIdHandler : IRequestHandler<TaskStatusGetByIdModel, Domain.Models.TaskStatus>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusGetByIdHandler(ITaskStatusService _service)
        {
            this._service = _service;   
        }
        public async Task<Domain.Models.TaskStatus> Handle(TaskStatusGetByIdModel request, CancellationToken cancellationToken)
        {
            var TaskStatusGetInDb= await _service.GetById(request.Id);
            return TaskStatusGetInDb;
        }
    }
}
