using Application.InterfacesCrudServices;
using Domain.Models.Authtification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesModelServices
{
    public interface IPermissionService:IGetAllService<Permission>
    {
    }
}
