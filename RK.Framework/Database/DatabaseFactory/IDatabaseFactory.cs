using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.Framework.Database.DatabaseFactory
{
    public interface IDatabaseFactory : IDisposable
    {
        RkDbContext GetDbContext();
    }
}
