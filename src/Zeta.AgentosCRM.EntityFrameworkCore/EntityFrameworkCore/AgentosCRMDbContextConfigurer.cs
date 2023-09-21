using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Zeta.AgentosCRM.EntityFrameworkCore
{
    public static class AgentosCRMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AgentosCRMDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AgentosCRMDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}