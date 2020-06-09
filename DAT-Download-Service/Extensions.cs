using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DAT_Download_Service
{
    public static class Extensions
    {
        public static IWebElement ValidateControl(this WebDriverWait wait, string value)
        {
            return wait.Until(e => e.FindElement(By.Id(value)));
        }
    }
}
