using Application.InterfacesModelServices;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForOrders
{
    public class OrderDeleteModel : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class OrderDeleteHandler: IRequestHandler<OrderDeleteModel, bool>
    {
        public readonly IMediator _mediator;
        public readonly IOrderService _orderService;

        public OrderDeleteHandler(IMediator mediator,IOrderService orderService)
        {
            _mediator = mediator;
            _orderService = orderService;   
        }
        public Task<bool> Handle(OrderDeleteModel request, CancellationToken cancellationToken)
        {
            var orderDelete = _orderService.Delete(request.Id);

            return orderDelete;
        }
    }
}
