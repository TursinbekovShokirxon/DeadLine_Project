using Domain.Models;
using Infrastructure.Handlers.ForOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineService.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<string>> OrderCreate(OrderCreateModel request)
        {
            var order = await _mediator.Send(request);

            if (order != null)
                return CreatedAtAction(nameof(OrderGetByIdAsync), new { id = order.Id }, "Заказ был создан");

            return BadRequest("Заказ не может быть создан");
        }

        [HttpGet]
        public async Task<ActionResult<Order>> OrderGetByIdAsync(OrderGetByIdModel request)
        {
            var order = await _mediator.Send(request);

            if (order == null)
                return NotFound($"Заказ с id = {request.Id} не найден");

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> OrderGetAll(OrderGetAllModel request)
        {
            var orderList = await _mediator.Send(request);

            if (!orderList.Any())
                return NotFound(new { Message = "Список заказов пуст" });

            return Ok(orderList);
        }


        [HttpDelete]
        public async Task<ActionResult<string>> OrderDeleteAsync(OrderDeleteModel request)
        {
            var orderDelete = await _mediator.Send(request);

            if (orderDelete)
                return Ok("Удаление заказа завершено");

            return BadRequest("Удаление заказа провалено!");
        }
    }
}
