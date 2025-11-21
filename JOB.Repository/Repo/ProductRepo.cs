using Booking.Core.Dtos;
using Booking.Core.Entity;
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
    public class ProductRepo : IProductRepo
    {
        private readonly AppDBContextIdentity _dbcontext;

        public ProductRepo(AppDBContextIdentity dbcontext)
        {
            _dbcontext = dbcontext;
        }





        public async Task<IEnumerable<Product>> GetAllAsync(int categoryId)
        {

            return await _dbcontext.Products
                .Where(p => p.CategoryId == categoryId).ToListAsync();

        }





        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbcontext.Products.FindAsync(id);
        }






        public async Task DeleteAsync(int id)
        {
            var product = await _dbcontext.Products.FindAsync(id);
            if (product != null)
            {
                _dbcontext.Products.Remove(product);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Product with id {id} not found");
            }
        }





        public async Task AddProduct(ProductToreturnDto product , String UserId)
        {
            Product newproduct  = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Availability = true,
                UserId = UserId
            };
            _dbcontext.Products.Add(newproduct);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<Product> UpdateProduct(int id , UpdateProductReq newproduct)
        {
            var OldProduct = await _dbcontext.Products.FindAsync(id);
            if (OldProduct == null)
            {
                throw new Exception($"Product with id {id} not found");
            }
            OldProduct.Name = newproduct.Name;
            OldProduct.Description = newproduct.Description;
            OldProduct.Price = newproduct.Price;
            OldProduct.Availability = newproduct.Availability;
            await _dbcontext.SaveChangesAsync();
            return OldProduct;
        }
    }
}
