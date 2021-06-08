using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ApplicationCore.RepositoryInterfaces
{
    // common CRUD operations
    public interface IAsyncRepository<T> where T: class
    {
        T GetById(int id);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(Expression<Func<T, bool>> filter);
        int GetCount(Expression<Func<T, bool>> filter);
        bool GetExists(Expression<Func<T, bool>> filter);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
