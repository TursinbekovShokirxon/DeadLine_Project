using Application.InterfacesModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusDeleteModel:IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class TaskStatusDeleteHandler : IRequestHandler<TaskStatusDeleteModel, bool>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusDeleteHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<bool> Handle(TaskStatusDeleteModel request, CancellationToken cancellationToken)
        {
           var res= await _service.Delete(request.Id);
            return res!=null;
        }
    }
}
