using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForRoles
{

    public class CreateRoleModel:IRequest<Role>
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Permission>? Permissions { get; set; }
    }
    public class CreateRoleHandler : IRequestHandler<CreateRoleModel, Role>
    {
        private readonly IRoleService _roleService;
        public CreateRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Role> Handle(CreateRoleModel request, CancellationToken cancellationToken)
        {
            var role = new Role() { 
                Name = request.Name
            };
            Role roleServ = await _roleService.Create(role);
            return roleServ;
        }
    }
}
