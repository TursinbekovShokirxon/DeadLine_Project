using Application.InterfacesCrudServices;
using Domain.Models;

namespace Application.InterfacesModelServices
{
    public interface IOrderService: 
        ICreateService<Order>,
        IGetAllService<Order>, 
        IGetByIdService<Order>
    {
        public Task<bool> Update(Order obj);
        public Task<bool> Delete(int id);
    }
}
