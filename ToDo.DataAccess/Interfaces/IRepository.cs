using System;
using System.Linq;
using System.Linq.Expressions;

namespace ToDo.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByConditions(Expression<Func<T, bool>> exp);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}