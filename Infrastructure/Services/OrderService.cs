using Domain.Models;
using Infrastructure.Contexts;
using MediatR;

namespace Infrastructure.Services
{
    public class OrderService
    {

        private readonly AppDbContext _db;
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator, AppDbContext db)
        {
            _mediator = mediator;
            _db = db;
        }

        public async Task<Order> Create(Order obj)
        {
            await _db.Orders.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var additionalEntity = await _db.Orders.FindAsync(id);
            if (additionalEntity == null)
                return false;

            _db.Orders.Remove(additionalEntity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders.ToList();
        }

        public async Task<Order> GetById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<bool> Update(Order obj)
        {
            Order additionalEntity = await GetById(obj.Id);
            if (additionalEntity == null) return false;

            additionalEntity.dategiven = obj.dategiven;
            additionalEntity.Price = obj.Price;
            additionalEntity.TaskId = obj.TaskId;
            additionalEntity.Task = obj.Task;
            additionalEntity.UserId = obj.UserId;
            additionalEntity.User = obj.User;

            _db.Orders.Update(additionalEntity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}


