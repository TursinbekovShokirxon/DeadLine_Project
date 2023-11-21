using Application.InterfacesCrudServices;
using Domain.Models;

namespace Application.ModelServices
{
    public interface IUserService : ICreateService<User>,IGetAllService<User>, IGetByIdService<User>
    {
        public Task<bool> Update(User obj);
        public Task<bool> Delete(int id);
    }
}
