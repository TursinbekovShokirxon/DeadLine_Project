namespace Application
{
    public interface ITaskService: 
        ICreateService<Domain.Models.Task>,
        IGetAllService<Domain.Models.Task>,
        IGetByIdService<Domain.Models.Task>
    {
        public Task<bool> Update(Domain.Models.Task obj);
        public Task<bool> Delete(int id);
    }
}
