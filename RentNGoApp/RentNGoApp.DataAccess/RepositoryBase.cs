using Microsoft.EntityFrameworkCore;
using RentNGoApp.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataAccess
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RentNGoAppContext _rentNGoAppContext { get; set; }

        public RepositoryBase(RentNGoAppContext rentNGoAppContext)
        {
            _rentNGoAppContext = rentNGoAppContext;
        }

        public IQueryable<T> FindAll()
        {
            return _rentNGoAppContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _rentNGoAppContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _rentNGoAppContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _rentNGoAppContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _rentNGoAppContext.Set<T>().Remove(entity);
        }
    }
}
