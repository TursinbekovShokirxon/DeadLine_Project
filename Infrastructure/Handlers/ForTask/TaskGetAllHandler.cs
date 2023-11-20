using Application.ModelServices;
using Infrastructure.Handlers.Task;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTask
{
    public class TaskGetAllModel:IRequest<IEnumerable<Domain.Models.Task>>
    {
    }
    public class TaskGetAllHandler : IRequestHandler<TaskGetAllModel, IEnumerable<Domain.Models.Task>>
    {
        private readonly ITaskService _service;
        public TaskGetAllHandler(ITaskService _service)
        {
            this._service = _service;
        }

        public async Task<IEnumerable<Domain.Models.Task>> 
            Handle(TaskGetAllModel request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Models.Task> taskList = _service.GetAll();

            return taskList;
        }
    }
}
