using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.EF.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private bool _disposed;

        private readonly InmetricsContext _context;

        protected DatabaseFacade DataBase => _context.Database;

        public BaseRepository(InmetricsContext context)
        {
            _context = context;
        }

        public void SaveChanges(int userId = 1, string userName = "System", bool saveLog = true)
        {
            _context.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
      
        protected TEntity ExecuteQuery(Expression<Func<TEntity, bool>> where = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool tracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.FirstOrDefault(where); ;
        }

        protected IEnumerable<TEntity> ExecuteQueryToList(Expression<Func<TEntity, bool>> where = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  int? skip = null,
                                  int? take = null,
                                  bool tracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }


        protected TResult ExecuteQuery<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> where = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (selector == null)
                throw new Exception("select can't be null");

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }


            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }

        protected IEnumerable<TResult> ExecuteQueryToList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> where = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  int? skip = null,
                                                  int? take = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (selector == null)
                throw new Exception("select can't be null");

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).ToList();
            }
            else
            {
                return query.Select(selector).ToList();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
