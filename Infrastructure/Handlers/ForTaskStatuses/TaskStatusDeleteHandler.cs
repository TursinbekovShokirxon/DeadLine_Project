using Application.InterfacesModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusDeleteModel:IRequest<string>
    {
        public int Id { get; set; }
    }
    public class TaskStatusDeleteHandler : IRequestHandler<TaskStatusDeleteModel, string>
    {
        private readonly ITaskStatusService _service;
        public TaskStatusDeleteHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<string> Handle(TaskStatusDeleteModel request, CancellationToken cancellationToken)
        {
           var res= await _service.Delete(request.Id);
            return res!=null?"Задание удалено":"Задание не может быть удалено";
        }
    }
}
