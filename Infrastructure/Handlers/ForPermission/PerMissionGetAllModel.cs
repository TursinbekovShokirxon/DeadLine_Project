using Application.InterfacesModelServices;
using Domain.Models.Authtification;
using MediatR;

namespace Infrastructure.Handlers.ForPermission
{
    public class PerMissionGetAllModel:IRequest<IEnumerable<Permission>>{}
    public class PermissionGetAllHandler : IRequestHandler<PerMissionGetAllModel, IEnumerable<Permission>>
    {
        private readonly IPermissionService _service;
        public PermissionGetAllHandler(IPermissionService service)
        {
             _service=service;
        }
        async Task<IEnumerable<Permission>> IRequestHandler<PerMissionGetAllModel, IEnumerable<Permission>>.Handle(PerMissionGetAllModel request, CancellationToken cancellationToken)
        {
            return _service.GetAll();
        }
    }

}
