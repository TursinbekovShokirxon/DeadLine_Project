using Application.ModelServices;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Task
{
    public class TaskGetByIdModel : IRequest<Domain.Models.Task>
    {
        public int Id { get; set; }
    }
    public class TaskGetByIdHandler : IRequestHandler<TaskGetByIdModel, Domain.Models.Task>
    {
        private readonly ITaskService _service;
        public TaskGetByIdHandler(ITaskService _service)
        {
            this._service = _service;
        }
        public async Task<Domain.Models.Task> Handle(TaskGetByIdModel request, CancellationToken cancellationToken)
        {
            var task = await _service.GetById(request.Id);
            return task;
        }
    }
}
