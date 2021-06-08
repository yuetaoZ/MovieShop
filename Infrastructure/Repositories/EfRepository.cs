using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            return entity;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = _dbContext.Set<T>().FindAsync(id);
            return await entity;
        }

        public virtual int GetCount(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).Count();
        }

        public Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetExists(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).Any();
        }

        public Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).ToList();
        }

        public virtual IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public Task<IEnumerable<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public virtual T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
