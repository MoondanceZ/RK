using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using RK.Framework.Database.UnitOfWork;

namespace RK.Service.Impl
{
    public class BaseService<T, TRepository>
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
    }
}
