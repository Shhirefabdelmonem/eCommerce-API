﻿using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repos
{
    internal class GenericRepo<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
    {
        public async Task<int> AddAsync(TEntity entity)
        {
           context.Set<TEntity>().Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity= await context.Set<TEntity>().FindAsync(id);
            if (entity is null)
                return 0;
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var res = await context.Set<TEntity>().FindAsync(id);
            return res; // res!
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
