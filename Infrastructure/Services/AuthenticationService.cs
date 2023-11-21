using Application.ModelServices;
using Domain.Models;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthenticationService : IUserAuthService
    {
        private readonly AppDbContext _db;
        public AuthenticationService(AppDbContext _db) => this._db = _db;
        
        public async Task<UserAuth?> Create(UserAuth obj)
        {
            var UserHasInDB = GetByUsername(obj.Username);
            if (UserHasInDB == null)
            {
                await _db.UserAuthifications.AddAsync(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
         return null;
        }
        public Task<UserAuth?> GetById(int Id)
        {
           var result= GetAll().FirstOrDefault(x=>x.UserId==Id);
            return System.Threading.Tasks.Task.FromResult(result);
        }
        public  UserAuth GetByUsername(string name)
        {
            var result =   GetAll().FirstOrDefault(x => x.Username == name);
            return result;
        }
        public IEnumerable<UserAuth> GetAll()
        {
            var result = _db.UserAuthifications;
            return  result.AsNoTracking();
        }
    }
}
