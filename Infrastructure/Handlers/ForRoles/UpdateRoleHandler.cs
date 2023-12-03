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
    public class UpdateRoleModel:IRequest<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<UserAuth>? UserAuthes { get; set; }
        public virtual ICollection<Permission>? Permissions { get; set; }
    }
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleModel, Role>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Role> Handle(UpdateRoleModel request, CancellationToken cancellationToken)
        {
            Role role = new()
            {
                  Id = request.Id,
                  Name = request.Name,
                  UserAuthes = request.UserAuthes,
                  Permissions = request.Permissions
            };
            Role update = await _roleService.Update(role);

            return role;

        }
    }
}
