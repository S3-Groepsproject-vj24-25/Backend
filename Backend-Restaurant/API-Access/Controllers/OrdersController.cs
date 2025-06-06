using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace API_Access.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }


        [HttpGet("kitchen")]
        public async Task<IActionResult> GetKitchenOrders()
        {
            var orders = await _orderService.GetOrdersByType("Food");
            return Ok(orders);
        }

        [HttpGet("bar")]
        public async Task<IActionResult> GetBarOrders()
        {
            var orders = await _orderService.GetOrdersByType("Drink");
            return Ok(orders);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderService.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return NoContent();
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartOrderPreparation(int id)
        {
            await _orderService.StartOrderPreparation(id);
            return NoContent();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            await _orderService.CompleteOrder(id);
            return NoContent();
        }
    }
}
