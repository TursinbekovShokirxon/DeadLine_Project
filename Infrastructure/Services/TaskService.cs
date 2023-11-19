using Application;
using Infrastructure.Contexts;
using MediatR;

namespace Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;
        private readonly IMediator _mediator;
        public TaskService(IMediator _mediator, AppDbContext _db)
        {
            this._mediator = _mediator;
            this._db = _db;
        }


        public async Task<string> Create(Domain.Task obj)
        {
                await _db.Tasks.AddAsync(obj);
                await _db.SaveChangesAsync();
            return "Task created";
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null)
                return false;

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Domain.Task> GetAll()
        {
            return _db.Tasks.ToList();
        }

        public async Task<Domain.Task> GetById(int id)
        {
            return await _db.Tasks.FindAsync(id);
        }

        public async Task<bool> Update(Domain.Task obj)
        {
            Domain.Task task = await GetById(obj.Id);
            if (task == null)
            {
                task.Price = obj.Price;
                task.Descryption = obj.Descryption;
                task.Deadline = obj.Deadline;
                task.User = obj.User;
                _db.Tasks.Update(task);
                await _db.SaveChangesAsync();
            }
            return task == null ? false : true;
        }

    }
}
