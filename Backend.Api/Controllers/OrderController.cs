using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Shared.Dto;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService userService)
        {
            _orderService = userService;
        }

        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto orderDto)
        {
            return Ok(await _orderService.CreateOrderAsync(orderDto));
        }

        [HttpPut("Change")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeOrderAsync([FromBody] OrderDto orderDto)
        {
            return Ok(await _orderService.ChangeOrderAsync(orderDto));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOrderAsync([FromBody] int orderId)
        {
            return Ok(await _orderService.DeleteOrderAsync(orderId));
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await _orderService.GetAllOrders());
        }
    }
}
