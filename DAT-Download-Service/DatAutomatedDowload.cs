using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;
using ExcelDataReader;
using System.IO;
using System.Linq;
using AutoMapper;
using DAT_Download_Service.Mapping;
using DAT_Download_Service.Models;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace DAT_Download_Service
{
    public static class DatAutomatedDowload
    {
        public static string siteUrl = "https://rates.test.dat.com/app/login.html#/";
        public static string multiLane = "https://rates.test.dat.com/app/#/multilane";
        public static IWebDriver webDriver = new ChromeDriver();
        public static string[] loginControlIds = new string[] { "username", "password", "login" };
        public static string[] loginControlValues = new string[] { "covenantcxn1", "Connexion1", "" };
        public static string fileName = "RequestLanesTemplate.csv";
        public static string filePath = @"C:\Users\EBAENA\Downloads\";
        public static string requestTemplatePath = @"C:\Users\EBAENA\Downloads\";
        public static string timeZone = "Pacific Standard Time";
        public static EventLog systemLog;

        public static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile(new MapperProfile());
        });

        public static void RunWebScraping(EventLog logger)
        {
            systemLog = logger;

            try
            {
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                webDriver.Navigate().GoToUrl(siteUrl);
                Thread.Sleep(5000);
                LoginAutomated();
                Thread.Sleep(5000);
                webDriver.Navigate().GoToUrl(multiLane);
                Thread.Sleep(10000);
                UploadLanesTemplate();
                Thread.Sleep(30000);
                ReviewWindow();
                Thread.Sleep(30000);
                SubmitRequestWindow();
                Thread.Sleep(30000);
                DownloadFile();
                Thread.Sleep(30000);
                webDriver.Close();
                Thread.Sleep(5000);
                ExcelReader();
            }
            catch (Exception ex)
            {
                webDriver.Close();
                webDriver.Quit();
                systemLog.WriteEntry($"Error in RunWebScraping: {ex.Message}");
            }
        }

        /// <summary>
        /// Manage the basic commands to run the login.
        /// </summary>
        static void LoginAutomated()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

                for (int i = 0; i < loginControlIds.Length; i++)
                {
                    string control = loginControlIds[i];
                    string value = loginControlValues[i];

                    IWebElement element = wait.ValidateControl(control);

                    if (!string.IsNullOrEmpty(value))
                    {
                        element.SendKeys(value);
                    }
                    else
                    {
                        element.Click();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"LoginAutomated: {ex.Message}");
            }
        }

        static void UploadLanesTemplate()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.ValidateControl("uploadBtn");
                element.Click();
                Thread.Sleep(1000);
                AutoItX3 autoIt = new AutoItX3();
                autoIt.ControlFocus("Open", "", "Edit1");
                Thread.Sleep(1000);
                autoIt.ControlSetText("Open", "", "Edit1", requestTemplatePath + fileName);
                Thread.Sleep(1000);
                autoIt.ControlClick("Open", "", "Button1");
            }
            catch (Exception ex)
            {
                throw new Exception($"UploadLanesTemplate: {ex.Message}");
            }
        }

        static void ReviewWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button")));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"ReviewWindow: {ex.Message}");
            }
        }

        static void SubmitRequestWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
                IWebElement chckElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='submitRequestForm']/div[10]/label[1]")));
                chckElement.Click();

                //Select spot rate parameters for the file
                IWebElement webElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='qa-select--spot-balance']/div[1]")));
                webElement.Click();
                Actions spotKeyDown = new Actions(webDriver);
                spotKeyDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter).Perform();

                IWebElement spotMktElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='submitRequestForm']/div[7]/div[2]/span/span/div[1]/div[1]/div[1]")));
                spotMktElement.Click();
                Actions spotMktDown = new Actions(webDriver);
                spotMktDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Enter).Perform();

                IWebElement timeFrame = wait.Until(e => e.FindElement(By.XPath("//*[@id='qa-select--spot-minimum-timeframe']/div[1]/div[1]")));
                timeFrame.Click();
                Actions spotTimeFrame = new Actions(webDriver);
                spotTimeFrame.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter).Perform();

                //Select contract rate parameters for the file
                IWebElement listElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='qa-select--contract-balance']")));
                listElement.Click();
                Actions rateKeyDown = new Actions(webDriver);
                rateKeyDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter).Perform();

                IWebElement mktElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='submitRequestForm']/div[10]/div[3]/span/span/div[1]/div[1]/div[2]")));
                mktElement.Click();
                Actions mktDown = new Actions(webDriver);
                mktDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Enter).Perform();


                IWebElement button = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button[2]")));
                button.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"SubmitRequestWindow: {ex.Message}");
            }
        }

        static void DownloadFile()
        {
            try
            {
                Thread.Sleep(120000);
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[1]/div[2]/div[1]/table/tbody/tr/td[8]/div[1]/div/div[1]/div[2]/span[2]/a")));
                element.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"DownloadFile: {ex.Message}");
            }
        }

        static void ExcelReader()
        {
            try
            {
                TimeZoneInfo Pacific_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime dateTime_Pacific = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Pacific_Standard_Time);
                var dateString = dateTime_Pacific.ToString("yyyyMMdd");

                DirectoryInfo dir = new DirectoryInfo(filePath);

                string partialName = $"{fileName}-{dateString}";
                FileInfo[] file = dir.GetFiles(partialName + "*.csv", SearchOption.TopDirectoryOnly).OrderByDescending(p => p.CreationTimeUtc).ToArray();

                if (file == null || file.Length <= 0)
                {

                }

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                FileStream stream = File.Open(file[0].FullName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream);
                ExcelDataSetConfiguration excelDataSetConfiguration = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = excelReader.AsDataSet(excelDataSetConfiguration);
                var dataTable = dataSet.Tables[0];

                IMapper mapper = config.CreateMapper();
                List<DataRow> rows = new List<DataRow>(dataTable.Rows.OfType<DataRow>());
                List<DATRateData> result = mapper.Map<List<DataRow>, List<DATRateData>>(rows);

                using (var context = new DownloadContext())
                {
                    context.DatRatesData.AddRange(result);
                    context.SaveChanges();
                }

                excelReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"ExcelReader: {ex.Message}");
            }
            
        }

    }

}
