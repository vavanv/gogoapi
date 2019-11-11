using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Services.Entities;
using Services.UnitOfWork;

namespace Services.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public Repository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public DbContext GetContext()
        {
            return _context;
        }

        public virtual void Add(TEntity instance)
        {
            _context.Set<TEntity>().Add(instance);
        }

        public virtual void Remove(TEntity instance)
        {
            _context.Set<TEntity>().Remove(instance);
        }

        public void Update(TEntity instance)
        {
            _context.Set<TEntity>().Update(instance);
        }

        public void Attach(TEntity instance)
        {
            _context.Set<TEntity>().Attach(instance);
        }

        public virtual async Task<TEntity> First()
        {
            return await _context.Set<TEntity>().FirstAsync();
        }

        public virtual async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).FirstAsync();
        }

        public virtual async Task<ICollection<TEntity>> All()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, string>> orderBy)
        {
            return await _context.Set<TEntity>().Where(predicate).AsNoTracking().OrderBy(orderBy).ToListAsync();
        }

        public virtual async Task<int> Count()
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync();
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync(predicate);
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
        }
    }
}