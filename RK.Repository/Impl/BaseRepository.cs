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
        protected readonly RkDbContext DataContext;
        protected readonly DbSet<T> Dbset;

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        //protected RkDbContext DataContext
        //{
        //    get
        //    {
        //        return dataContext;
        //    }
        //}
        
        public BaseRepository(IDatabaseFactory databaseFactory)
        {            
            DatabaseFactory = databaseFactory;
            DataContext = databaseFactory.DataContext;
            Dbset = DataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            Dbset.Add(entity);
        }

        //新增方法
        public virtual void AddAll(IEnumerable<T> entities)
        {
            Dbset.AddRange(entities);
        }
        public virtual void BulkInsert(IEnumerable<T> entities)
        {
            DataContext.BulkInsert(entities);
        }
        public virtual void Update(T entity)
        {
            Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        //新增方法
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T obj in entities)
            {
                Dbset.Attach(obj);
                DataContext.Entry(obj).State = EntityState.Modified;
            }
        }
        public virtual void BulkUpdate(IEnumerable<T> entities)
        {
            DataContext.BulkUpdate(entities);
        }
        public virtual void Delete(T entity)
        {
            Dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = Dbset.Where<T>(predicate).AsEnumerable();
            Dbset.RemoveRange(objects);
        }

        //新增方法
        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            Dbset.RemoveRange(entities);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate).ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> GetAllLazy()
        {
            return Dbset.AsQueryable();
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Any(predicate);
        }
    }
}
