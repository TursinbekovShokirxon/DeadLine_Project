using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _db;
        public PermissionService(AppDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Permission> GetAll()
        {
            return _db.Permissions.ToList();
        }
    }
}
