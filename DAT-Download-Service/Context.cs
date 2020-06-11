using DAT_Download_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DAT_Download_Service
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-S6LCQIHP\SQLDEV_2019;User ID=sa;Password=Odragde.2020;Database=DatApi;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<DATRateData> DatRatesData { get; set; }
    }
}
