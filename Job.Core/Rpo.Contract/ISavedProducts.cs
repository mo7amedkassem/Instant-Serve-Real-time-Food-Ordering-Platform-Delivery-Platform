using Booking.Core.Dtos;
using Booking.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Rpo.Contract
{
    public interface ISavedProducts
    {
        Task SaveProduct(string userId, int productId);
        Task<IEnumerable<SavedProductsDto>> GetAllSavedProductsByUserIdAsync(string userId);

        Task DeleteSavedProduct(int id);
    }
}
