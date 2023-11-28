using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using MediatR;

namespace Infrastructure.Handlers.ForRoles
{
    public class GetByIdRoleModel:IRequest<Role>
    {
        public int Id { get; set; }
    }
    public class GetByIdRoleHandler : IRequestHandler<GetByIdRoleModel, Role>
    {
        private readonly IRoleService _roleService;
        public GetByIdRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<Role> Handle(GetByIdRoleModel request, CancellationToken cancellationToken)
        {
         return await _roleService.GetById(request.Id);
        }
    }
}
