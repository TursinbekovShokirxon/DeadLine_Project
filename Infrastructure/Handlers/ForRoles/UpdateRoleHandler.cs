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
        public Task<Role> Handle(UpdateRoleModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
