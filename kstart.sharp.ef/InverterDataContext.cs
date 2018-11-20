using kstar.sharp.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace kstar.sharp.ef
{
    public class InverterDataContext : DbContext
    {
        public DbSet<InverterDataGranular> InverterDataGranular { get; set; }

        private string connectionString = @"Data Source=c:\databases\inverter-data.db";

        public InverterDataContext()
        {

        }


        public InverterDataContext(DbContextOptions<InverterDataContext> options)
        {
            var opt = options.FindExtension<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>();

            if (opt != null)
            {
                connectionString = opt.ConnectionString;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }


}
