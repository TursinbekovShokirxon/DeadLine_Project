using Application.InterfacesModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForRoles
{

    public class DeleteRoleModel:IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleModel, bool>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<bool> Handle(DeleteRoleModel request, CancellationToken cancellationToken)
        {
            var delete = await _roleService.Delete(request.Id);
            return delete;
        }
    }
}
