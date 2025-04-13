using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Entities;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {

        private readonly OrderServices _orderServices;

        public OrderController(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderServices.GetOrdersAsync();
            if (orders == null)
            {
                return NotFound("Sipariş Bulunamadı");
            }
            return Ok(orders);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderServices.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Sipariş Bulunamadı");
            }
            return Ok(order);
        }

        [HttpPost]

        public async Task <IActionResult> CreateOrder([FromBody] List<Order> order)
        {
            if(order == null || order.Count == 0)
            {
                return BadRequest("Geçersiz sipariş ");
            }
            var createdOrder = await _orderServices.CreateOrderAsync(order);
            return Ok(createdOrder);
        }



    }
}