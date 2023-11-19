using Domain.Models;

namespace Application
{
    public interface IUserService: ICreateService<User>
    {
        public Task<bool> Update(User obj);
        public Task<User> GetById(int id);
        public IEnumerable<User> GetAll();
        public Task<bool> Delete(int id);
    }
}
