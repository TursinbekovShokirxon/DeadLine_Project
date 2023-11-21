using Application.InterfacesModelServices;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForOrders
{
    public class OrderGetAllModel : IRequest<IEnumerable<Order>>
    {

    }
    public class OrderGetAllHandler : IRequestHandler<OrderGetAllModel, IEnumerable<Order>>
    {
        private readonly IOrderService _orderService;
        public OrderGetAllHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IEnumerable<Order>> Handle(OrderGetAllModel request, CancellationToken cancellationToken)
        {
            return _orderService.GetAll();
        }
    }
}
