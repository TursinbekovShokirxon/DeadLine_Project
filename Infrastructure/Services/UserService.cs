using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain.Models;
using Infrastructure.Contexts;
using MediatR;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMediator _mediator;

        public UserService(IMediator mediator, AppDbContext db)
        {
            _mediator = mediator;
            _db = db;
        }

        public async Task<string> Create(User obj)
        {
            // Логика создания пользователя
            await _db.Users.AddAsync(obj);
            await _db.SaveChangesAsync();
            return "User created";
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public async Task<User> GetById(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<bool> Update(User obj)
        {
            var user = await GetById(obj.Id);
            if (user == null)
                return false;

            // Логика обновления свойств пользователя
            user.NickName = obj.NickName;
            user.Universty = obj.Universty;
            user.Course = obj.Course;
            user.Faculty = obj.Faculty;
            user.Budget = obj.Budget;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
