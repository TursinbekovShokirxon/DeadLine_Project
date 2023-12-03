using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.VisualStudio.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForRoles
{
    public class AddPermissionInRoleModel:IRequest<string>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
    public class AddPermissionInRoleHandler : IRequestHandler<AddPermissionInRoleModel, string>
    {
        private readonly IPermissionService _PermissionService;
        private readonly IRoleService _RoleService;
        private readonly AppDbContext _db;
        public AddPermissionInRoleHandler(IPermissionService PermissionService,IRoleService RoleService, AppDbContext db)
        {
            _PermissionService = PermissionService;
            _RoleService = RoleService;
            _db = db;
        }
        
        public async Task<string> Handle(AddPermissionInRoleModel request, CancellationToken cancellationToken)
        {
         var Permission=  _PermissionService.GetAll().FirstOrDefault(x=>x.Id==request.PermissionId);
            if (Permission == null) return "Такого разрешения нету";
         var Role = _RoleService.GetAll().FirstOrDefault(x=>x.Id==request.RoleId);
            if (Role == null) return "Такого роля нету";
            
             Role?.Permissions?.Add(Permission);
            _db.SaveChanges();
            
            return $"{Permission} Разрешение добавлен в {Role} роль";
        }
    }
}
