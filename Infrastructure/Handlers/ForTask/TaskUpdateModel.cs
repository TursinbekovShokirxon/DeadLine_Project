using Application.ModelServices;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTask
{
    public class TaskUpdateModel:IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Descryption { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
    }
    public class TaskUpdateModelHandler : IRequestHandler<TaskUpdateModel, bool>
    {
        private readonly ITaskService _service;
        public TaskUpdateModelHandler(ITaskService _service)
        {
            this._service = _service;
        }

        public async Task<bool> Handle(TaskUpdateModel request, CancellationToken cancellationToken)
        {
            Domain.Models.Task task = new()
            {
                Id = request.Id,
                Name = request.Name,
                Descryption = request.Descryption,
                Deadline = request.Deadline,
                UserId = request.UserId
            };

            var taskUpdate = await _service.Update(task);

            return taskUpdate;
        }
    }
}
