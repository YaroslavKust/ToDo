using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Interfaces;

namespace ToDo.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ToDoContext Context;

        public Repository(ToDoContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll() => Context.Set<T>().AsNoTracking();

        public IQueryable<T> GetByConditions(Expression<Func<T, bool>> exp) => 
            Context.Set<T>().Where(exp).AsNoTracking();

        public void Create(T entity) => Context.Set<T>().Add(entity);

        public void Update(T entity) => Context.Set<T>().Update(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);
    }
}