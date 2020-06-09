using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Timers;
using ST = System.Timers;

namespace DAT_Download_Service
{
    public partial class DatDownloadService : ServiceBase
    {
        private ST.Timer eventTimer = new ST.Timer();

        public DatDownloadService()
        {
            InitializeComponent();
            logger = new EventLog();
            if (!EventLog.SourceExists("DatDownloadService"))
            {
                EventLog.CreateEventSource("DatDownloadService", "DatServiceLog");
            }
            logger.Source = "DatDownloadService";
            logger.Log = "DatServiceLog";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                eventTimer = new ST.Timer();

                string interval = ConfigurationManager.AppSettings["ExecutionInterval"].ToString();

                this.eventTimer.Interval = Convert.ToInt32(interval, CultureInfo.InvariantCulture);
                this.eventTimer.Elapsed += new ST.ElapsedEventHandler(this.eventTimer_Tick);
                eventTimer.Enabled = true;
                logger.WriteEntry("DatDownloadService has Started.");
            }
            catch (Exception ex)
            {
                logger.WriteEntry("Error on DatDownloadService: " + ex.Message);
            }
        }

        protected override void OnStop()
        {
            logger.WriteEntry("DatDownloadService has Stopped.");
        }

        private void eventTimer_Tick(object sender, ElapsedEventArgs e)
        {
            DatAutomatedDowload.RunWebScraping();
        }
    }
}
