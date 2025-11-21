using AutoMapper;
using Booking.Core.Dtos;
using Booking.Core.Rpo.Contract;
using Booking.Repository.Data.DBIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JOB_PORTALl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrdersRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly AppDBContextIdentity _appDBContext;

        public OrderController(IOrdersRepo orderRepo , IMapper mapper , AppDBContextIdentity appDBContext)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _appDBContext = appDBContext;
        }

        [HttpPost("Buy")]
        public async Task<IActionResult> Buy(OrderDto order)
        {
            await _orderRepo.BuyAsync(order);

            var product = await _appDBContext.Products
                .Where(p => p.ID == order.ProductId)
                .Select(p => new { p.Name, p.Price })
                .FirstOrDefaultAsync();

            return Ok(new
            {
                message = "Order placed successfully.",
                product = product?.Name,
                price = product?.Price
            });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepo.DeleteAsync(id);
            return Ok(new { message = "Order deleted successfully." });
        }

        [HttpPost("UpdateStatus/{orderId}")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] string status)
        {
            await _orderRepo.UpdateStatus(orderId, status);
            return Ok(new { message = "Order status updated successfully." });
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
