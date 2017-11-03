using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using RK.Framework.Database;

namespace RK.Repository.Impl
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
