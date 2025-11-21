using Booking.Core.Entity;
using Booking.Core.Pero_Contract;
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
    public class GenaricRepoo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly AppDBContextIdentity _dbcontext;

        public GenaricRepoo( AppDBContextIdentity dbcontext )
        {
           _dbcontext = dbcontext;
        }

         


        public async Task AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }




        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }




        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }




        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found");
            }
            _dbcontext.Set<T>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }


    }
}
