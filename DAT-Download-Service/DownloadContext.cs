using DAT_Download_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DAT_Download_Service
{
    public class DownloadContext : DbContext
    {
        public DownloadContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLDEV_2019;User ID=sa;Password=Odragde.2020;Database=DatApi;");
        }

        public DbSet<DATRateData> DatRatesData { get; set; }
    }
}
