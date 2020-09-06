using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Domain.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);

        Task<QueryResult<TEntity>> GetAsync(Query bQuery,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        TEntity InsertOrUpdate(TEntity entity);

    }
}
