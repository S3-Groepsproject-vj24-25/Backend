using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Access.Controllers
{
    [ApiController]
    public class OrdersController : Controller
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("kitchen")]
        public IActionResult GetKitchenOrders()
        {
            var orders = _orderService.GetOrdersByType("Food");
            return Ok(orders);
        }

        [HttpGet("bar")]
        public IActionResult GetBarOrders()
        {
            var orders = _orderService.GetOrdersByType("Drink");
            return Ok(orders);
        }

        [HttpGet("status")]
        public IActionResult GetOrdersByStatus(string status)
        {
            var orders = _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("add")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            _orderService.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }

        [HttpPost("{id}/start")]
        public IActionResult StartOrderPreparation(int id)
        {
            _orderService.StartOrderPreparation(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public IActionResult CompleteOrder(int id)
        {
            _orderService.CompleteOrder(id);
            return NoContent();
        }

    }
}
