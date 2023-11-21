using Application.InterfacesModelServices;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TaskStatusService:ITaskStatusService
    {
        private readonly AppDbContext _db;
        private readonly IMediator _mediator;

        public TaskStatusService(IMediator mediator, AppDbContext db)
        {
            _mediator = mediator;
            _db = db;
        }

        public async Task<Domain.Models.TaskStatus> Create(Domain.Models.TaskStatus obj)
        {
            var existingStatus = await _db.TaskStatuses.FindAsync(obj.Id);
            if (existingStatus != null)
            {
                _db.Entry(existingStatus).State = EntityState.Detached;
                _db.TaskStatuses.Attach(existingStatus);
                return existingStatus;
            }
            await _db.TaskStatuses.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var taskStatus = await _db.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
                return false;

            _db.TaskStatuses.Remove(taskStatus);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Domain.Models.TaskStatus> GetAll()
        {
            return _db.TaskStatuses.ToList();
        }

        public async Task<Domain.Models.TaskStatus?> GetById(int id)
        {
            return await _db.TaskStatuses.FindAsync(id);
        }

        public async Task<bool> Update(Domain.Models.TaskStatus obj)
        {
            var existingStatus = await _db.TaskStatuses.FindAsync(obj.Id);
            if (existingStatus == null) return false;

            existingStatus.StatusName = obj.StatusName;

            _db.TaskStatuses.Update(existingStatus);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}