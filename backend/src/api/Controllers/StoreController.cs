using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Abstractions.Dtos;
using PetStore.Services.Abstractions.Requests.Store;
using PetStore.Services.Abstractions.Services;

namespace PetStore.Api.Controllers
{
    [ApiController]
    [Route("store")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("order")]
        public async Task<ActionResult<IList<OrderDto>>> GetAllOrders(CancellationToken ct)
            => Ok(await _storeService.GetAllOrdersAsync(ct));

        [HttpGet("inventory")]
        public async Task<ActionResult<IDictionary<string, int>>> GetInventory(CancellationToken ct)
            => Ok(await _storeService.GetInventoryAsync(ct));

        [HttpPost("order")]
        public async Task<ActionResult<OrderDto>> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken ct)
            => Ok(await _storeService.PlaceOrderAsync(request, ct));

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid orderId, CancellationToken ct)
        {
            var order = await _storeService.GetOrderByIdAsync(orderId, ct);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpDelete("order/{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken ct)
        {
            var result = await _storeService.DeleteOrderAsync(orderId, ct);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
