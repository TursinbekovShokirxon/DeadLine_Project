using Application.ModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForTask
{
    public class TaskDeleteModel:IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class TaskDeleteHandler : IRequestHandler<TaskDeleteModel, bool>
    {
        private readonly ITaskService _service;
        public TaskDeleteHandler(ITaskService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(TaskDeleteModel request, CancellationToken cancellationToken)
        {
            var taskDelete = await _service.Delete(request.Id);

            return taskDelete;
        }
    }

}
