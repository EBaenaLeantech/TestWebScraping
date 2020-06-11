using AutoMapper;
using DAT_Download_Service.Mapping;
using System.ServiceProcess;

namespace DAT_Download_Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DatDownloadService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
