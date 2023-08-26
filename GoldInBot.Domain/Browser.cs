using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldInBot.Domain
{
    public class Browser
    {
        public IWebDriver driver;

        public Browser()
        {
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();
        }

        public async Task Navigate(Uri url)
        {
            this.driver.Navigate().GoToUrl(url);
        }
    }
}
