using Domain;

namespace Application
{
    public interface IUserService
    {
        public Task<string> Create(User obj);
        public Task<bool> Update(User obj);
        public Task<User> GetById(int id);
        public IEnumerable<User> GetAll();
        public Task<bool> Delete(int id);
    }
}
