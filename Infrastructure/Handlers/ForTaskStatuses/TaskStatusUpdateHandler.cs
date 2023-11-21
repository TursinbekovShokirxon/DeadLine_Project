using Application.InterfacesModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{ 
    public class TaskStatusUpdateModel:IRequest<bool>
    {
        public Domain.Models.TaskStatus TaskStatus { get; set; }
    }
    public class TaskStatusUpdateHandler : IRequestHandler<TaskStatusUpdateModel, bool>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusUpdateHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<bool> Handle(TaskStatusUpdateModel request, CancellationToken cancellationToken)
        {
            var res=await _service.Update(request.TaskStatus);
            return res;
        }
    }
}
