using kstar.sharp.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace kstar.sharp.ef
{
    public class InverterDataContext : DbContext
    {
        public InverterDataContext(DbContextOptions<InverterDataContext> options)
            : base(options)
        { }

        public DbSet<InverterDataGranular> InverterDataGranular { get; set; }



        //private string connectionString = @"Data Source=inverter-data.db";

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(connectionString);
        //}
    }


}
