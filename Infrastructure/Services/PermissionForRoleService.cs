using Application.CustomeAuth;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PermissionForRoleService : IPermissionForRoleService
    {
        private readonly AppDbContext _db;
        public PermissionForRoleService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<HashSet<string>> GetPermissionAsync(int memberId)
        {
            ICollection<Role>[] roles = await _db.UserAuthifications
                .Where(x => x.UserId == 2)
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Select(x => x.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(x=>x)
                .SelectMany(x=>x.Permissions)
                .Select(x=>x.Name)
                .ToHashSet();
        }
    }
}
