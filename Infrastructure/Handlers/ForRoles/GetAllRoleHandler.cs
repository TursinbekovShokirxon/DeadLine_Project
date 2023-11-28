using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using MediatR;

namespace Infrastructure.Handlers.ForRoles
{
    public class GetAllRoleModel : IRequest<IEnumerable<Role>> { 
        
    }
    public class GetAllHandler : IRequestHandler<GetAllRoleModel, IEnumerable<Role>>
    {
        private readonly IRoleService _roleService;
        public GetAllHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IEnumerable<Role>> Handle(GetAllRoleModel request, CancellationToken cancellationToken)
        {
            var res = _roleService.GetAll().ToList();
            return res;
        }
    }
}
