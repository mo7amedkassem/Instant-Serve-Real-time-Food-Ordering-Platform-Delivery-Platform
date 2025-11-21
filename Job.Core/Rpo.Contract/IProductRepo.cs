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

    public interface IProductRepo 
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(int categoryId);

        Task AddProduct(ProductToreturnDto product , String UserId);
        Task DeleteAsync(int id);

        Task<Product> UpdateProduct(int id , UpdateProductReq newproduct);
    }

}
