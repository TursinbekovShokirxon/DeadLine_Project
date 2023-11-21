using Application.InterfacesModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{ 
    public class TaskStatusUpdateModel:IRequest<string>
    {
        public Domain.Models.TaskStatus TaskStatus { get; set; }
    }
    public class TaskStatusUpdateHandler : IRequestHandler<TaskStatusUpdateModel, string>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusUpdateHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<string> Handle(TaskStatusUpdateModel request, CancellationToken cancellationToken)
        {
            var res=await _service.Update(request.TaskStatus);
            return res==true?"Статус успешно обновлен":"не может быть обновлен";
        }
    }
}
