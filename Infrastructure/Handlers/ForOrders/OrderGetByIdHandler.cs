using Application.InterfacesModelServices;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForOrders
{
    public class OrderGetByIdModel : IRequest<Order>
    {
        public int Id { get; set; }
    }
    public class OrderGetByIdHandler : IRequestHandler<OrderGetByIdModel, Order>
    {
        private readonly IOrderService _orderService;
        public OrderGetByIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Order> Handle(OrderGetByIdModel request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(request.Id);

            return order;
        }
    }
}
