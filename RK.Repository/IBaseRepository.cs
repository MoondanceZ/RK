using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RK.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        bool IsExist(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddAll(IEnumerable<T> entities);
        void BulkInsert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void BulkUpdate(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void DeleteAll(IEnumerable<T> entities);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllLazy();
    }
}
