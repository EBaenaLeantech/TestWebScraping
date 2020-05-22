using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

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
                Thread.Sleep(3000);
                LoginAutomated();
                Thread.Sleep(3000);
                webDriver.Navigate().GoToUrl(multiLane);
                Thread.Sleep(3000);
                UploadLanesTemplate();
                Thread.Sleep(2000);
                ReviewWindow();
                Thread.Sleep(2000);
                SubmitRequestWindow();
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
                autoIt.ControlSetText("Open", "", "Edit1", @"C:\Users\EBAENA\Downloads\Request lanes template.csv");
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
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
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
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath("//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button[2]")));
                element.Click();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
