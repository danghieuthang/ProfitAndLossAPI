using ProfitAndLoss.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        TEntity GetById(TKey id);
        TEntity GetById(TKey id, Expression<Func<TEntity, object>> include);
        IQueryable<TEntity> Entity();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TKey id);
        TEntity Delete(TEntity entity);
        void DeleteMulti(List<TEntity> entities);

        void Dispose();
        void Commit();
        void CommitAsync();
    }

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>, IDisposable where TEntity : BaseEntity<TKey>
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> dbSet;
        private bool _disposed;
        public BaseRepository(DataContext context)
        {
            this._context = _context ?? context;
            this.dbSet = _context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void CommitAsync()
        {
            _context.SaveChangesAsync();
        }

        public TEntity Delete(TKey id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                return dbSet.Remove(entity).Entity;
            }
            return null;
        }

        public TEntity Delete(TEntity entity)
        {
            return dbSet.Remove(entity).Entity;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IQueryable<TEntity> Entity()
        {
            return dbSet;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return dbSet;
            }
            return dbSet.Where(expression);
        }

        public TEntity GetById(TKey id)
        {
            return dbSet.Find(id);
        }

        public TEntity GetById(TKey id, Expression<Func<TEntity, object>> include)
        {
            // return dbSet.Where(include).fi
            return dbSet.Include(include).FirstOrDefault(c => c.Id.Equals(id));
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteMulti(List<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
    }
}
