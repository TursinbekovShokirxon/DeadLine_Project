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
    public class GetAllRoleModel:IRequest<IEnumerable<Role>>
    {
    }
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleModel, IEnumerable<Role>>
    {
        private readonly IRoleService _rolesServices;
        public async Task<IEnumerable<Role>> Handle(GetAllRoleModel request, CancellationToken cancellationToken)
        {
            var res = _rolesServices.GetAll();
            return res;
        }
    }
}
