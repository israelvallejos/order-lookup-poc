using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderLookup.API.DTOs;
using OrderLookup.API.Interfaces;
using OrderLookup.API.Services;

namespace OrderLookup.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{orderNumber}")]
    public async Task<ActionResult<OrderDetailResponse>> GetOrder(string orderNumber)
    {
        var order = await _orderService.GetOrderByNumberAsync(orderNumber);
        
        if (order == null)
            return NotFound(new { message = $"Order '{orderNumber}' not found" });

        return Ok(order);
    }

    [HttpPost("search")]
    public async Task<ActionResult<OrderDetailResponse>> SearchOrder([FromBody] OrderSearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OrderNumber))
            return BadRequest(new { message = "Order number is required" });

        var order = await _orderService.GetOrderByNumberAsync(request.OrderNumber.Trim());
        
        if (order == null)
            return NotFound(new { message = $"Order '{request.OrderNumber}' not found" });

        return Ok(order);
    }
}
