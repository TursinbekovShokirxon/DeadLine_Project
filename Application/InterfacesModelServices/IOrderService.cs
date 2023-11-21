using Application.InterfacesCrudServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesModelServices
{
    public interface IOrderService: 
        ICreateService<Order>,
        IGetAllService<Order>, 
        IGetByIdService<Order>
    {
    }
}
