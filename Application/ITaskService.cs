namespace Application
{
    public interface ITaskService
    {
        public Task<string> Create(Domain.Task obj);
        public Task<bool> Update(Domain.Task obj);
        public Task<Domain.Task> GetById(int id);
        public IEnumerable<Domain.Task> GetAll();
        public Task<bool> Delete(int id);
    }
}
