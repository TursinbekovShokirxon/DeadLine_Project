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
    public class UserAuthenticationModel:IRequest<UserAuth> {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserAuthenticationHandler : IRequestHandler<UserAuthenticationModel, UserAuth>
    {
        public Task<UserAuth> Handle(UserAuthenticationModel request, CancellationToken cancellationToken)
        {
        
        }
    }
}
