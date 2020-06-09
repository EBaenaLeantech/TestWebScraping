using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace DAT_Download_Service
{
    public static class DatAutomatedDowload
    {
        public static string siteUrl = "https://rates.test.dat.com/app/login.html#/";
        public static string multiLane = "https://rates.test.dat.com/app/#/multilane";
        public static IWebDriver webDriver = new ChromeDriver();
        public static string[] loginControlIds = new string[] { "username", "password", "login" };
        public static string[] loginControlValues = new string[] { "covenantcxn1", "Connexion1", "" };
        public static string requestTemplatePath = @"C:\Users\EBAENA\Downloads\Request lanes template.csv";
        public static string reviewWindowBtnNext = "//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button";
        public static string submitWindowBtnAccept = "//*[@id='main']/div/div[2]/div/div[1]/footer/div[3]/button[2]";

        public static void RunWebScraping()
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
            webDriver.Dispose();
            webDriver.Close();
        }

        /// <summary>
        /// Manage the basic commands to run the login.
        /// </summary>
        private static void LoginAutomated()
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

        /// <summary>
        /// Manage the basic commands to run the login.
        /// </summary>
        private static void UploadLanesTemplate()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.ValidateControl("uploadBtn");
                element.Click();
                Thread.Sleep(1000);
                AutoItX3 autoIt = new AutoItX3();
                autoIt.ControlFocus("Open", "", "Edit1");
                Thread.Sleep(2000);
                autoIt.ControlSetText("Open", "", "Edit1", requestTemplatePath);
                Thread.Sleep(2000);
                autoIt.ControlClick("Open", "", "Button1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Manage the basic commands to run the login.
        /// </summary>
        private static void ReviewWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath(reviewWindowBtnNext)));
                element.Click();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Manage the basic commands to run the login.
        /// </summary>
        private static void SubmitRequestWindow()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(e => e.FindElement(By.XPath(submitWindowBtnAccept)));
                element.Click();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
