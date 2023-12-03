using GoldInBot.Domain.Helpers;
using GoldInBot.Domain.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldInBot.Domain.Components;

namespace GoldInBot.Domain.Pages
{
    public class ResultPage : IGoldInPage
    {
        private readonly Browser _browser;

        private string PeopleButtonXPath = "//button[text()='People']";
        public IWebElement? PeopleButton => this._browser.driver.FindElement(PeopleButtonXPath);

        private string PeopleButtonSelectedXPath = "//button[text()='People' and contains(@class, 'selected')]";
        public bool PeopleButtonSelected => this._browser.driver.FindElement(PeopleButtonSelectedXPath) is not null;

        private string JobsButtonXPath = "//button[text()='Jobs']";
        public IWebElement? JobsButton => this._browser.driver.FindElement(JobsButtonXPath);

        private string JobsButtonSelectedXPath = "//button[text()='Jobs' and contains(@class, 'selected')]";
        public bool JobsButtonSelected => this._browser.driver.FindElement(JobsButtonSelectedXPath) is not null;

        private string PostsButtonXPath = "//button[text()='Posts']";
        public IWebElement? PostsButton => this._browser.driver.FindElement(PostsButtonXPath);

        private string CompaniesButtonXPath = "//button[text()='Companies']";
        public IWebElement? CompaniesButton => this._browser.driver.FindElement(CompaniesButtonXPath);

        private string GroupsButtonXPath = "//button[text()='Groups']";
        public IWebElement? GroupsButton => this._browser.driver.FindElement(GroupsButtonXPath);

        private string SchoolsButtonXPath = "//button[text()='Schools']";
        public IWebElement? SchoolsButton => this._browser.driver.FindElement(SchoolsButtonXPath);

        private string CoursesButtonXPath = "//button[text()='Courses']";
        public IWebElement? CoursesButton => this._browser.driver.FindElement(CoursesButtonXPath);

        private string EventsButtonXPath = "//button[text()='Events']";
        public IWebElement? EventsButton => this._browser.driver.FindElement(EventsButtonXPath);

        private string ProductsButtonXPath = "//button[text()='Products']";
        public IWebElement? ProductsButton => this._browser.driver.FindElement(ProductsButtonXPath);

        private string ServicesButtonXPath = "//button[text()='Services']";
        public IWebElement? ServicesButton => this._browser.driver.FindElement(ServicesButtonXPath);

        private string AllFiltersButtonXPath = "//button[text()='All filters']";
        public IWebElement? AllFiltersButton => this._browser.driver.FindElement(AllFiltersButtonXPath);

        private string NextButtonXPath = "//button[@aria-label='Next']";
        public IWebElement? NextButton => this._browser.driver.FindElement(NextButtonXPath);

        
        private string ConnectionsButtonXPath = "//button[text()='Connections']";
        public IWebElement? ConnectionsButton => this._browser.driver.FindElement(ConnectionsButtonXPath);

        private string ConnectionsCancelButtonXPath = "//span[text()='1st']/../../../../../../div[2]/button[1]";
        public IWebElement? ConnectionsCancelButton => this._browser.driver.FindElement(ConnectionsCancelButtonXPath);

        private string ConnectionsShowResultsButtonXPath = "//span[text()='1st']/../../../../../../div[2]/button[2]";
        public IWebElement? ConnectionsShowResultsButton => this._browser.driver.FindElement(ConnectionsShowResultsButtonXPath);


        private string ConnectionLevel1CheckXPath = "//span[text()='1st']/../../../label";
        public IWebElement? ConnectionLevel1Check => this._browser.driver.FindElement(ConnectionLevel1CheckXPath);

        private string ConnectionLevel2CheckXPath = "//span[text()='2nd']/../../../label";
        public IWebElement? ConnectionLevel2Check => this._browser.driver.FindElement(ConnectionLevel2CheckXPath);

        private string ConnectionLevel3CheckXPath = "//span[text()='3rd+']/../../../label";
        public IWebElement? ConnectionLevel3Check => this._browser.driver.FindElement(ConnectionLevel3CheckXPath);



        public ResultPage(Browser browser)
        {
            _browser = browser;
        }

        public async Task EnsurePageLoaded()
        {
            int count = 0;

            if (NextButton is null)
            {
                while (NextButton is null)
                {
                    await Task.Delay(GlobalConsts.DefaultWaitTimeForEachTryMs);

                    this._browser.driver.ScrollToBottom();

                    count++;

                    if (count * GlobalConsts.DefaultWaitTimeForEachTryMs >= GlobalConsts.DefaultWaitTimeForLoadPageMs)
                        break;
                }
            }
            else
            {
                await Task.Delay(GlobalConsts.DefaultWaitTimeForLoadPageMs);
            }
        }

        public bool CurrentPage => this._browser.driver.Url.Contains("linkedin.com/search/results");

        public async Task ChangeToPeopleResult()
        {
            if (!PeopleButtonSelected)
            {
                PeopleButton?.Click();
                await this.EnsurePageLoaded();
            }
        }

        public async Task SetConnectionLevel(ConnectionLevelFlag levels)
        {

            if (levels == ConnectionLevelFlag.None)
                return;

            this.ConnectionsButton?.Click();

            if (levels.HasFlag(ConnectionLevelFlag.First))
                this.ConnectionLevel1Check?.Click();

            if (levels.HasFlag(ConnectionLevelFlag.Second))
                this.ConnectionLevel2Check?.Click();

            if (levels.HasFlag(ConnectionLevelFlag.Third))
                this.ConnectionLevel3Check?.Click();

            this.ConnectionsShowResultsButton?.Click();

            await this.EnsurePageLoaded();
        }

        public async Task ConnectEveryBodyOnPageResult()
        {
            int count = 0;
            while (count < 2)
            {
                try
                {
                    if (count > 0 && this._browser.driver.FindElement("//h2[contains(@class,'limit-alert')]") is not null)
                        this._browser.driver.FindElement("//h2[contains(@class,'limit-alert')]/../../div[3]/button")?.Click();

                    var lst = await this.GetPeopleResultListItems();

                    foreach (var i in lst.Where(x => x.HasConnectButton).ToList())
                        await this.Connect(i.ConnectButton!);

                    break;
                }
                catch
                {
                    count++;
                    await Task.Delay(GlobalConsts.DefaultWaitTimeForLoadPageMs);
                }
            }
        }

        public async Task NextPageResult()
        {
            if (NextButton is null)
                return;

            if (!NextButton.Enabled)
                return;

            NextButton?.Click();
            await this.EnsurePageLoaded();
        }

        private Task Connect(IWebElement connectButton)
        {
            connectButton?.Click();

            var yourInventationIsAlmostOnItsWay = this._browser.driver.FindElement("//h2[@Id='send-invite-modal']");
            if (yourInventationIsAlmostOnItsWay is not null)
            {
                var sendNowButton = this._browser.driver.FindElement("//button[@aria-label='Send now']");
                sendNowButton?.Click();
            }

            return Task.CompletedTask;
        }

        public async Task<List<PeopleResultItem>> GetPeopleResultListItems()
        {
            this._browser.driver.ScrollToTop();

            var lst = new List<PeopleResultItem>();

            List<IWebElement> findElements = new List<IWebElement>();

            int count = 0;

            while (findElements.Count == 0)
            {
                findElements = this._browser.driver.FindElements("//main[@aria-label='Search results']/div/div/div/div/ul/li");
                await Task.Delay(GlobalConsts.DefaultWaitTimeForEachTryMs);

                count++;

                if (count * GlobalConsts.DefaultWaitTimeForEachTryMs >= GlobalConsts.DefaultWaitTimeForLoadPageMs)
                    break;
            }

            findElements = findElements.Take(findElements.Count - 1).ToList();

            foreach (var i in findElements)
                lst.Add(new PeopleResultItem(i));

            return lst;
        }
    }
}
