using Booking.Core.Dtos;
using Booking.Core.Pero_Contract;
using Job.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Rpo.Contract
{
    public interface IOrdersRepo 
    {
        Task UpdateStatus(int orderId, string status);
        Task<IEnumerable<Order>> GetAllOrdersByUserIdAsync(string userId);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task DeleteAsync(int id);
        Task BuyAsync(OrderDto order);
    }
}
