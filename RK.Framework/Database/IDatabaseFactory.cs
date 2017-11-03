using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Framework.Database
{
    public interface IDatabaseFactory
    {
        RkDbContext DataContext { get; }
    }
}
