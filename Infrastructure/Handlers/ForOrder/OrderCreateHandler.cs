using Application.InterfacesModelServices;
using Application.ModelServices;
using Domain.Models;
using Infrastructure.Handlers.ForUser;
using Infrastructure.Handlers.Task;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForOrders
{
    public class OrderCreateModel : IRequest<Order>
    {
        public string Price { get; set; } = string.Empty;

        public int TaskId { get; set; }

        public int UserId { get; set; }
    }
    public class OrderCreateHandler : IRequestHandler<OrderCreateModel, Order>
    {
        public readonly IOrderService _orderService;
        public readonly ITaskService _taskService;
        public readonly IMediator _mediator;

        public OrderCreateHandler(IMediator mediator,IOrderService orderService)
        {
            _mediator = mediator;
            _orderService = orderService;
        }
        public async Task<Order> Handle(OrderCreateModel request, CancellationToken cancellationToken)
        {
            Task<Domain.Models.Task> task = _mediator.Send(new TaskGetByIdModel()
            {
                Id = request.TaskId
            });
            if (task == null) return null;

            Task<User> user = _mediator.Send(new UserGetByIdModel()
            {
                Id = request.UserId
            });

            if (user == null) return null;

            Order orderModel = new Order()
            {
                dateCreated = DateTime.Now,
                Price = request.Price,
                TaskId = request.TaskId,
                UserId = user.Id
            };

            Order order = await _orderService.Create(orderModel);

            return order;
        }
    }
}
