using System;
using System.Linq;
using System.Linq.Expressions;
using CollectionsManager.DAL.EF;
using CollectionsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext DbContext;

        protected RepositoryBase(AppDbContext repositoryContext)
        {
            DbContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
            => trackChanges ? DbContext.Set<T>() : DbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            var query = DbContext.Set<T>().Where(expression);

            return trackChanges ? query : query.AsNoTracking();
        }

        public void Create(T entity)
            => DbContext.Set<T>().Add(entity);

        public void Update(T entity)
            => DbContext.Set<T>().Update(entity);

        public void Delete(T entity)
            => DbContext.Set<T>().Remove(entity);
    }
}
