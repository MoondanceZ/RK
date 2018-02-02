using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using RK.Framework.Database;
using Microsoft.AspNetCore.Http;
using RK.Infrastructure;

namespace RK.Service.Impl
{
    public class BaseService<T, TRepository>
        where T : class
        where TRepository : IBaseRepository<T>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly TRepository _repository;
        private readonly IHttpContextAccessor _httpContext;

        public BaseService(IUnitOfWork unitOfWork, TRepository repository, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _httpContext = httpContext;
        }

        /// <summary>
        /// 水平越权校验
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected bool CheckCurrentUserValid(int userId)
        {
            return _httpContext.HttpContext.User.HasClaim("sub", EncryptHelper.AESEncrypt(userId.ToString()));
        }
    }
}
