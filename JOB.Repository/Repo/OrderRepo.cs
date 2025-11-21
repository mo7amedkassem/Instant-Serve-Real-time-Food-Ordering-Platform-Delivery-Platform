using Booking.Core.Dtos;
using Booking.Core.Pero_Contract;
using Booking.Core.Rpo.Contract;
using Booking.Repository.Data.DBIdentity;
using Job.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Repo
{
    public class OrderRepo : IOrdersRepo
    {
        private readonly AppDBContextIdentity _context;

        public OrderRepo(AppDBContextIdentity Context) 
        {
            _context = Context;
        }

        public async Task BuyAsync(OrderDto order )
        {
            var Buy = new Order
            {
                Status = order.Status,
                UserId = order.UserId,
                ProductId = order.ProductId,
            };
            _context.Orders.Add(Buy);
            await _context.SaveChangesAsync();
        }
        

        public async Task DeleteAsync(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Order with id {id} not found");
            }
        }


        public async Task<IEnumerable<Order>> GetAllAsync()
        { 
            return await _context.Orders.ToListAsync();
        }


        public Task<IEnumerable<Order>> GetAllOrdersByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                return Task.FromResult(order);
            }
            throw new Exception($"Order with id {id} not found");
        }

        public Task UpdateStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }
            throw new Exception($"Order with id {orderId} not found");
        }
    }
}
