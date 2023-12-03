using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace GoldInBot.Domain.Helpers
{
    public static class WebElementHelper
    {
        public static IWebElement? FindElement(this IWebDriver driver, string xpath)
        {
            try
            {
                return driver.FindElement(By.XPath(xpath));
            }
            catch
            {
                return null;
            }
        }

        public static List<IWebElement> FindElements(this IWebDriver driver, string xpath)
        {
            try
            {
                return driver.FindElements(By.XPath(xpath)).ToList();
            }
            catch
            {
                return new List<IWebElement>();
            }
        }

        public static IWebElement? FindElement(this IWebElement element, string xpath)
        {
            try
            {
                return element.FindElement(By.XPath(xpath));
            }
            catch
            {
                return null;
            }
        }

        public static List<IWebElement> FindElements(this IWebElement element, string xpath)
        {
            try
            {
                return element.FindElements(By.XPath(xpath)).ToList();
            }
            catch
            {
                return new List<IWebElement>();
            }
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");
        }
    }
}
