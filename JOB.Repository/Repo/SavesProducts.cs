using Booking.Core.Dtos;
using Booking.Core.Entity;
using Booking.Core.Rpo.Contract;
using Booking.Repository.Data.DBIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Repo
{
    public class SavesProducts : ISavedProducts
    {
        private readonly AppDBContextIdentity _context;
        public SavesProducts(AppDBContextIdentity context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SavedProductsDto>> GetAllSavedProductsByUserIdAsync(string userId)
        {

            var userSavedProducts = await _context.SavedProducts
            .Include(sp => sp.Product) 
            .Where(sp => sp.UserId == userId).
            Select(sp => new SavedProductsDto
            {
                ID = sp.ID,
                Name = sp.Product.Name,
                Description = sp.Product.Description,
                Price = sp.Product.Price,
                category = sp.Product.category,
                CategoryId = sp.Product.CategoryId,
                UserId = sp.UserId,
                Availability = sp.Product.Availability
            })
            .ToListAsync();
            if (!userSavedProducts.Any())
            {
                throw new Exception("This user has no saved products");
            }
            return userSavedProducts;
        }




        public async Task SaveProduct(string userId, int productId)
        {
            var Product = await _context.Products.FindAsync(productId);

            if (Product == null)
            {
                throw new Exception("Product not found");
            }

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var alreadySaved = await _context.SavedProducts
                .AnyAsync(sp => sp.UserId == userId && sp.ProductId == productId);

            if (!alreadySaved)
            {
                var savedProduct = new SavedProduct
                {
                    UserId = userId,
                    ProductId = productId,
                    SavedAt = DateTime.UtcNow
                };
                _context.SavedProducts.Add(savedProduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product already saved");
            }
        }










        public async Task DeleteSavedProduct(int id)
        {
            var savedProduct = await _context.SavedProducts.FindAsync(id);
            if (savedProduct == null)
            {
                throw new Exception("Saved product not found");
            }
            _context.SavedProducts.Remove(savedProduct);
            await _context.SaveChangesAsync();
        }


    
    }
}
