using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA;
using OpenQA.Selenium.Interactions;
using ExcelDataReader;
using System.IO;


namespace TestWebScraping
{
    class Program
    {
        public static string siteUrl = "https://rates.test.dat.com/app/login.html#/";
        public static string multiLane = "https://rates.test.dat.com/app/#/multilane";
        public static IWebDriver webDriver = new ChromeDriver();
        public static string[] loginControlIds = new string[] { "username", "password", "login" };
        public static string[] loginControlValues = new string[] { "covenantcxn1", "Connexion1", "" };

        static void Main(string[] args)
        {
            Console.WriteLine("Web scraping being...");
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
                Thread.Sleep(10000);
                ReviewWindow();
                Thread.Sleep(10000);
                SubmitRequestWindow();
                Thread.Sleep(20000);
                DownloadFile();
                Thread.Sleep(5000);
                ExcelReader();
            }
            catch (Exception ex)
            {
                webDriver.Close();
                webDriver.Quit();
                Console.WriteLine($"Error Trying to login: {ex.Message}");
            }

            Console.ReadKey();
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
                Console.WriteLine($"Error Trying to login: {ex.Message}");
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
                autoIt.ControlSetText("Open", "", "Edit1", @"C:\Users\Orlando Galvez\Downloads\18225 matrix CSV File.csv");
                Thread.Sleep(1000);
                autoIt.ControlClick("Open", "", "Button1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ReviewWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(25));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button")));
                element.Click();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void SubmitRequestWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
                IWebElement chckElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='submitRequestForm']/div[10]/label[1]")));
                chckElement.Click();
                IWebElement listElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='qa-select--contract-balance']")));
                listElement.Click();
                Actions keyDown = new Actions(webDriver);
                keyDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Tab + Keys.Enter).Perform();
                IWebElement mktElement = wait.Until(e => e.FindElement(By.XPath("//*[@id='submitRequestForm']/div[10]/div[3]/span/span/div[1]/div[1]/div[2]")));
                mktElement.Click();
                Actions mktDown = new Actions(webDriver);
                mktDown.SendKeys(Keys.Tab + Keys.Tab + Keys.Enter).Perform();
                IWebElement button = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button[2]")));
                button.Click();
            }
            catch (Exception ex)
            {
                throw ex;
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

                throw ex;
            }
        }

        static void ExcelReader() 
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var filePath = @"C:\Users\Orlando Galvez\Downloads\18225 matrix CSV File.csv-202006030850.csv";
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
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
            excelReader.Close();
        }

    }

    public static class Extensions
    {
        public static IWebElement ValidateControl(this WebDriverWait wait, string value)
        {
            return wait.Until(e => e.FindElement(By.Id(value)));
        }

    }
}
