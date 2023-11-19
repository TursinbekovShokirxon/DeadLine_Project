using Application;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthificationService : IUserAuthService
    {
        private readonly AppDbContext _db;
        public AuthificationService(AppDbContext _db) => this._db = _db;
        
        public async Task<UserAuth> Create(UserAuth obj)
        {
            await _db.UserAuthsInformations.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
        public async Task<UserAuth> GetById(int Id)
        {
           var result= GetAll().FirstOrDefault(x=>x.UserId==Id);
            return result;
        }
        public IEnumerable<UserAuth> GetAll()
        {
            var result = _db.UserAuthsInformations;
            return  result.AsNoTracking();
        }
    }
}
