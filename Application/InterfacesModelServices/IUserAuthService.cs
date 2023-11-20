using Application.InterfacesCrudServices;
using Domain.Models.Authtification;

namespace Application.ModelServices
{
    public interface IUserAuthService :
        ICreateService<UserAuth>,
        IGetByIdService<UserAuth>,
        IGetAllService<UserAuth>
    {
        public UserAuth GetByUsername(string name);
    }
}
