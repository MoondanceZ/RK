using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Database.DatabaseFactory;

namespace RK.Repository.Impl
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        protected UserInfoRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
