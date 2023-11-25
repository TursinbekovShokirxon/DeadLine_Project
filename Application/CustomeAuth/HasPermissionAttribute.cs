using Domain.Models.Authtification;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomeAuth
{
    public class HasPermissionAttribute:AuthorizeAttribute
    {
        public HasPermissionAttribute(/*Permission permission*/string permission)
            :base(policy: permission)
        {

        }
    }
}
