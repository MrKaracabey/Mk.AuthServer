using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mk.AuthServer.Core.Repositories;


namespace Mk.AuthServer.API.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EfDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {//TODO : Dbden Null Gelme case ini Handle et
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                //TODO : EntityState.Detached ne demek araştır
                _context.Entry(entity).State = EntityState.Detached;
            }
            else
            {
                return null;
            }

            return entity;

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> @predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}