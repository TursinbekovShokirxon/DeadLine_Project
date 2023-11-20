using Application.ModelServices;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForUser
{
    public class UserGetAllModel : IRequest<IEnumerable<User>> { }
    public class UserGetAllHandler : IRequestHandler<UserGetAllModel, IEnumerable<User>>
    {
        private readonly IUserService _service;
        public UserGetAllHandler(IUserService _service)
        {
            this._service = _service;
        }
        public async Task<IEnumerable<User>> Handle(UserGetAllModel request, CancellationToken cancellationToken)
        {
            return _service.GetAll();
        }
    }
}
