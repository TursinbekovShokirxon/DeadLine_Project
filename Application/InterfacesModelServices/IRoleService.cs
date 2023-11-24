using Application.InterfacesCrudServices;
using Domain.Models.Authtification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesModelServices
{
    public interface IRoleService:IGetAllService<Role>,IGetByIdService<Role>,ICreateService<Role>
    {
        public Task<Role> Update(Role obj);
        public Task<bool> Delete(int Id);
    }
}
