using Application.InterfacesCrudServices;
using Domain.Models.Authtification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelServices
{
    public interface IUserAuthService :
        ICreateService<UserAuth>,
        IGetByIdService<UserAuth>,
        IGetAllService<UserAuth>
    {
        public Task<UserAuth> GetByUsername(string name);
    }
}
