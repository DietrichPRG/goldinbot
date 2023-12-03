using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldInBot.Domain;
using GoldInBot.Domain.Interfaces;
using GoldInBot.Domain.Pages;

namespace GoldInBot.UseCase
{
    public class HomeUseCase
    {
        private readonly HomePage _homePage;
        private const string HomeUrl = "https://www.linkedin.com/feed/";

        public HomeUseCase(Browser browser)
        {
            this._homePage = new HomePage(browser);

            if (!_homePage.CurrentPage)
                browser.driver.Navigate().GoToUrl(new Uri(HomeUrl));
        }

        public Task DoSearch(string search)
        {
            this._homePage.SearchInput?.SendKeys(search);
            this._homePage.SearchInput?.SendKeys(OpenQA.Selenium.Keys.Enter);
            this._homePage.EnsurePageLoaded();

            return Task.CompletedTask;
        }
        
    }
}
