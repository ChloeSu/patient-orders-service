using Microsoft.AspNetCore.Mvc;
using PatientOrdersService.Services.Interfaces;
using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/api/PatientOrders")]
        public async Task<IActionResult> GetOrdersByPatientId([FromQuery] GetOrdersByPatientIdReq req)
        {
            var result = await _orderService.GetOrdersByPatientIdAsync(req.PatientId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderReq req)
        {
            var affected = await _orderService.CreateOrderAsync(req);
            return affected > 0 ? Ok("Order created.") : BadRequest("Failed to create order.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderReq req)
        {
            var affected = await _orderService.UpdateOrderAsync(req);
            return affected > 0 ? Ok("Order updated.") : BadRequest("Failed to update order.");
        }
    }
}
