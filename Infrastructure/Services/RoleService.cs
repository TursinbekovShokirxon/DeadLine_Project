using Application.InterfacesModelServices;
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
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;
        public RoleService(AppDbContext db)
        {
            _db= db;
        }
        public async Task<Role> Create(Role obj)
        {
           await _db.Roles.AddAsync(obj);
           await _db.SaveChangesAsync();
           return obj;
        }

        public async Task<bool> Delete(int Id)
        {
           Role elem =await GetById(Id);
            _db.Roles.Remove(elem);
            return elem != null ? true : false;
        }

        public IEnumerable<Role> GetAll()
        {
            return _db.Roles.AsNoTracking();
        }

        public async Task<Role> GetById(int Id)
        {
            return  GetAll().FirstOrDefault(x => x.Id == Id);
        }

        public async Task<Role> Update(Role obj)
        {
            Role role = new();
            _db.Roles.Update(role);
            _db.SaveChanges();
            return obj;
        }
    }
}
