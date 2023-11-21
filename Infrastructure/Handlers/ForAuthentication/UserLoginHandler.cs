using Application.ModelServices;
using Domain.Models.Authtification;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForAuthentication
{
    public class UserLoginModel:IRequest<UserAuth> {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
    public class UserLoginHandler : IRequestHandler<UserLoginModel, UserAuth>
    {
        private readonly IUserAuthService _service;
        public UserLoginHandler(IUserAuthService _service) =>
            this._service = _service;

        public async Task<UserAuth> Handle(UserLoginModel request, CancellationToken cancellationToken)
        {
          var user = _service.GetByUsername(request.Username);
            return user;
        }
    }
}
