using Microsoft.AspNetCore.Authorization;
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
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            string? memberId = context.User.Claims.FirstOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Sub
                )?.Value;
            if (!Guid.TryParse(memberId, out Guid parsememberId) )
            {
                return;
            }
        }
    }
}
