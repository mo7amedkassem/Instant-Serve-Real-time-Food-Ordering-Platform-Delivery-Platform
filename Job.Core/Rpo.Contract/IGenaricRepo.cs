using Booking.Core.Entity;
using Job.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Pero_Contract
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task AddAsync(T entity);

    }
}
