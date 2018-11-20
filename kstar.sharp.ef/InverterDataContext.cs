using kstar.sharp.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace kstar.sharp.ef
{
    public class InverterDataContext : DbContext
    {

        private DbContextOptions<InverterDataContext> _options;
        public InverterDataContext(DbContextOptions<InverterDataContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<InverterDataGranular> InverterDataGranular { get; set; }


        public InverterDataContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }

        public InverterDataContext() { }


        private string _connectionString = @"Data Source=../sqlite/inverter-data.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
                optionsBuilder.UseSqlite(_connectionString);
        }
    }


}
