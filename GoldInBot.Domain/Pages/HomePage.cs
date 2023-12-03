using GoldInBot.Domain.Interfaces;
using OpenQA.Selenium;
using GoldInBot.Domain.Helpers;

namespace GoldInBot.Domain.Pages
{
    public class HomePage : IGoldInPage
    {
        private readonly Browser _browser;

        
        private const string LinkedinLogoButtonXPath = "//a[@class='app-aware-link']";
        public IWebElement? LinkedinLogoButton => this._browser.driver.FindElement(LinkedinLogoButtonXPath);

        private const string HomeButtonXPath = "//a[@class='app-aware-link  global-nav__primary-link--active global-nav__primary-link']";
        public IWebElement? HomeButton => this._browser.driver.FindElement(HomeButtonXPath);

        private const string MyNetWorkButtonXPath = "//a[@class='app-aware-link  global-nav__primary-link']/span[text() = 'My Network']";
        public IWebElement? MyNetWorkButton => this._browser.driver.FindElement(MyNetWorkButtonXPath);

        private const string JobsButtonXPath = "//a[@class='app-aware-link  global-nav__primary-link']/span[text() = 'Jobs']";
        public IWebElement? JobsButton => this._browser.driver.FindElement(JobsButtonXPath);

        private const string MessagingButtonXPath = "//a[@class='app-aware-link  global-nav__primary-link']/span[text() = 'Messaging']";
        public IWebElement? MessagingButton => this._browser.driver.FindElement(MessagingButtonXPath);

        private const string NotificationsButtonXPath = "//a[@class='app-aware-link  global-nav__primary-link']/span[text() = 'Notifications']";
        public IWebElement? NotificationsButton => this._browser.driver.FindElement(NotificationsButtonXPath);

        private const string MeButtonXPath = "//span[text()='Me']";
        public IWebElement? MeButton => this._browser.driver.FindElement(MeButtonXPath);

        private const string SearchInputXPath = "//input[@class='search-global-typeahead__input']";
        public IWebElement? SearchInput => this._browser.driver.FindElement(SearchInputXPath);

        private const string NextButtonXPath = "//span[@class='artdeco-button__text' and text()='Next']";
        public IWebElement? NextButton => this._browser.driver.FindElement(NextButtonXPath);


        public HomePage(Browser browser)
        {
            _browser = browser;
        }

        public Task EnsurePageLoaded()
        {
            int count = 0;
            while (NextButton is null)
            {
                Thread.Sleep(GlobalConsts.DefaultWaitTimeForEachTryMs);
                this._browser.driver.ScrollToBottom();

                count++;

                if (count * GlobalConsts.DefaultWaitTimeForEachTryMs >= GlobalConsts.DefaultWaitTimeForLoadPageMs)
                    break;
            }

            return Task.CompletedTask;
        }

        public bool CurrentPage => this._browser.driver.Url.Contains("linkedin.com/feed");
    }
}
