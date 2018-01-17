using EntityFramework.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RK.Framework.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RK.Repository.Impl
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly RkDbContext dataContext;
        private readonly DbSet<T> dbset;

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected RkDbContext DataContext
        {
            get
            {
                return dataContext;
            }
        }
        
        public BaseRepository(IDatabaseFactory databaseFactory)
        {            
            DatabaseFactory = databaseFactory;
            dataContext = databaseFactory.DataContext;
            dbset = DataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        //新增方法
        public virtual void AddAll(IEnumerable<T> entities)
        {
            dbset.AddRange(entities);
        }
        public virtual void BulkInsert(IEnumerable<T> entities)
        {
            dataContext.BulkInsert(entities);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        //新增方法
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T obj in entities)
            {
                dbset.Attach(obj);
                dataContext.Entry(obj).State = EntityState.Modified;
            }
        }
        public virtual void BulkUpdate(IEnumerable<T> entities)
        {
            dataContext.BulkUpdate(entities);
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = dbset.Where<T>(predicate).AsEnumerable();
            dbset.RemoveRange(objects);
        }

        //新增方法
        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return dbset.Where(predicate).ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return dbset.Where(predicate).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> GetAllLazy()
        {
            return dbset.AsQueryable();
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            return dbset.Any(predicate);
        }
    }
}
