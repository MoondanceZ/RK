using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using RK.Framework.Database.UnitOfWork;

namespace RK.Service.Impl
{
    public class BaseService<T, TRepository> : IBaseService<T>
        where T : class
        where TRepository : IBaseRepository<T>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly TRepository _repository;

        public BaseService(IUnitOfWork unitOfWork, TRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public void AddAll(IEnumerable<T> entities)
        {
            _repository.AddAll(entities);
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            _repository.BulkInsert(entities);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            _repository.Update(entities);
        }

        public void BulkUpdate(IEnumerable<T> entities)
        {
            _repository.BulkUpdate(entities);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            _repository.Delete(where);
        }

        public void DeleteAll(IEnumerable<T> entities)
        {
            _repository.DeleteAll(entities);
        }

        public T GetById(long Id)
        {
            return _repository.GetById(Id);
        }

        public T GetById(string Id)
        {
            return _repository.GetById(Id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _repository.Get(where);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public IQueryable<T> GetAllLazy()
        {
            return _repository.GetAllLazy();
        }
    }
}
