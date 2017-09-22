using Microsoft.EntityFrameworkCore;

namespace RK.Framework.Database
{
    public class RkDbContext : DbContext
    {
        public RkDbContext(DbContextOptions<RkDbContext> options)
            : base(options)
        {
        }
    }
}