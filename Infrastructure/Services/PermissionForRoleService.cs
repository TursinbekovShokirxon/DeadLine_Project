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
    public class PermissionForRoleService : IPermissionService
    {
        private readonly AppDbContext _db;
        public Task<HashSet<string>> GetPermissionAsync(Guid memberId)
        {
            _db.Set<UserAuth>()
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.UserId == 1)
                .Select(x => x.Roles)
                .ToArrayAsync();
        }
    }
}
