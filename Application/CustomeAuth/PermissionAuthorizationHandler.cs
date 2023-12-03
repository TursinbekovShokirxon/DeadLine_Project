using Application.ModelServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomeAuth
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        //private readonly IUserAuthService _userAuthService;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory/*, IUserAuthService userAuthService*/)
        {
            _serviceScopeFactory = serviceScopeFactory;
            //_userAuthService = userAuthService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            //string? memberId = context.User.Claims.FirstOrDefault(
            //    x => x.Type == JwtRegisteredClaimNames.Sub
            //    )?.Value;
            await Console.Out.WriteLineAsync(context.User.Identity.Name);
            //if (!Guid.TryParse(memberId, out Guid parsememberId) )
            //{
            //    return;
            //}

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPermissionForRoleService permissionService = scope.ServiceProvider
                .GetRequiredService<IPermissionForRoleService>();
            //if(context.User.Identity.Name == "J")
            //var userAuth = _userAuthService.GetByUsername(context.User.Identity.Name);
            
            var permission = await permissionService.GetPermissionAsync(2/*userAuth.UserId*/);
            

            if (permission.Contains(requirement.Permission)) {
                context.Succeed(requirement);
            }

        }
    }
}
