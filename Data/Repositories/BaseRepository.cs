using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LikeButton.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using LikeButton.Domain.IRepositories;
using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<QueryResult<TEntity>> GetAsync(Query bQuery,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                query = orderBy(query);
            }


            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            var totalItems = await query.CountAsync();

            if(bQuery == null)
            {
                bQuery = new Query(1, 10);
            }


            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            var entities = await query.Skip((bQuery.Page - 1) * bQuery.ItemsPerPage)
                .Take(bQuery.ItemsPerPage)
                .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database.
            return new QueryResult<TEntity>
            {
                Items = entities,
                TotalItems = totalItems,
            };
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                return Insert(entity);
            }
            else
            {
                Update(entity);
                return entity;
            }

        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
