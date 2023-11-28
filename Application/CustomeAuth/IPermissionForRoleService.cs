using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomeAuth
{
    public interface IPermissionForRoleService
    {
        Task<HashSet<string>> GetPermissionAsync(int memberId);
    } 
}
