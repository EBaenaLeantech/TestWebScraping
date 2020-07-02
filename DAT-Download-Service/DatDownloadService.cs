using Microsoft.Win32.TaskScheduler;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Timers;

namespace DAT_Download_Service
{
    public partial class DatDownloadService : ServiceBase
    {
        private Timer eventTimer = new Timer();
        private static string appPath = @"C:\Users\EBAENA\source\repos\TestWebScraping\TestWebScraping\bin\Debug\netcoreapp3.1\TestWebScraping.exe";
        private static string serviceUser = "LAPTOP-S6LCQIHP\\EBAENA";
        private static string servicePassword = "Odragde.2020";


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
                eventTimer = new Timer();

                string interval = ConfigurationManager.AppSettings["ExecutionInterval"].ToString();

                this.eventTimer.Interval = Convert.ToInt32(interval, CultureInfo.InvariantCulture);
                this.eventTimer.Elapsed += new ElapsedEventHandler(this.eventTimer_Tick);
                this.eventTimer.Start();
                logger.WriteEntry("DatDownloadService has Started.");
            }
            catch (Exception ex)
            {
                logger.WriteEntry("Error on DatDownloadService: " + ex.Message);
            }
        }

        protected override void OnStop()
        {
            this.eventTimer.Stop();
            logger.WriteEntry("DatDownloadService has Stopped.");
        }

        private void eventTimer_Tick(object sender, ElapsedEventArgs e)
        {
            logger.WriteEntry("Webscraping initiated.");
            try
            {
                CreateTaskSchedule();
            }
            catch (Exception ex)
            {
                logger.WriteEntry($"Error running CreateTaskSchedule: {ex.Message}");
            }

            logger.WriteEntry("Webscraping completed.");
        }

        static void CreateTaskSchedule()
        {
            try
            {
                TaskDefinition td = TaskService.Instance.NewTask();

                // Create a new task definition and assign properties
                td.RegistrationInfo.Description = "DAT Dowloader Service";
                td.Principal.LogonType = TaskLogonType.InteractiveToken;

                // Add a trigger that will fire the task at this time every other day
                MonthlyTrigger dt = (MonthlyTrigger)td.Triggers.Add(new MonthlyTrigger(15));
                dt.Repetition.Duration = TimeSpan.FromHours(1);
                //dt.StartBoundary = Convert.ToDateTime(DateTime.Now.Date.ToString() + " 00:00");

                // Add an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(appPath));

                // Register the task in the root folder
                const string taskName = "DATDownloader";
                TaskService.Instance.RootFolder.RegisterTaskDefinition(
                    taskName,
                    td,
                    TaskCreation.CreateOrUpdate,
                    serviceUser,
                    servicePassword,
                    TaskLogonType.InteractiveToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
