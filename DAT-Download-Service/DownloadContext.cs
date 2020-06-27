using DAT_Download_Service.Models;
using System.Data.Entity;

namespace DAT_Download_Service
{
    public class DownloadContext : DbContext
    {
        public DownloadContext():base("DownloadContext")
        {
        }

        public DbSet<DATRateData> DatRatesData { get; set; }
    }
}
