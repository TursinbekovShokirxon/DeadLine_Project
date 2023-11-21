using Application.InterfacesModelServices;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTaskStatuses
{
    public class TaskStatusGetAllModel : IRequest<IEnumerable<Domain.Models.TaskStatus>> { }
    public class TaskStatusGetAllHandler : IRequestHandler<TaskStatusGetAllModel, IEnumerable<Domain.Models.TaskStatus>>
    {

        private readonly ITaskStatusService _service;
        public TaskStatusGetAllHandler(ITaskStatusService _service)
        {
            this._service = _service;
        }
        public async Task<IEnumerable<Domain.Models.TaskStatus>> Handle(TaskStatusGetAllModel request, CancellationToken cancellationToken)
        {
            var tasks =  _service.GetAll();
            return  tasks;
        }
    }
}
