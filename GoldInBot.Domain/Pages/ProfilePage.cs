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
    public class ProfilePage : IGoldInPage
    {
        private readonly Browser _browser;

        private string H1NameXPath = "//a[contains(@href,'about-this-profile')]/h1";
        public IWebElement? H1Name => this._browser.driver.FindElement(H1NameXPath);

        private string DivSubTitleXPath = "//a[contains(@href,'about-this-profile')]/../../../div[2]";
        public IWebElement? DivSubTitle => this._browser.driver.FindElement(DivSubTitleXPath);

        private string SpanLocationXPath = "//a[contains(@href,'about-this-profile')]/../../../../div[2]/span";
        public IWebElement? SpanLocation => this._browser.driver.FindElement(SpanLocationXPath);

        private string AContactInfoXPath = "//a[contains(@href,'about-this-profile')]/../../../../div[2]/span";
        public IWebElement? AContactInfo => this._browser.driver.FindElement(AContactInfoXPath);

        private string ButtonAboutSeeMoreXPath = "//button[text()='…see more']";
        public IWebElement? ButtonAboutSeeMore => this._browser.driver.FindElement(ButtonAboutSeeMoreXPath);

        private string SpanAboutXPath = "//div[@id='about']/../div[3]/div/div/div/span";
        public IWebElement? SpanAbout => this._browser.driver.FindElement(SpanAboutXPath);

        private string ExperiencesXPath = "//div[@id='experience']/../div[3]/ul/li";
        public List<IWebElement> Experiences => this._browser.driver.FindElements(ExperiencesXPath);

        private string DivExperienceTitleXPath = "./div/div[2]/div/div/div/div/div/div/span";
        public IWebElement? DivExperienceTitle(IWebElement? li) => li?.FindElement(DivExperienceTitleXPath);

        private string SpanExperienceSubTitleXPath = "./div/div[2]/div/div/span[1]";
        public IWebElement? SpanExperienceSubTitle(IWebElement? li) => li?.FindElement(SpanExperienceSubTitleXPath);

        private string SpanExperiencePeriodXPath = "./div/div[2]/div/div/span[2]";
        public IWebElement? SpanExperiencePeriod(IWebElement? li) => li?.FindElement(SpanExperiencePeriodXPath);

        private string SpanExperienceLocationXPath = "./div/div[2]/div/div/span[3]";
        public IWebElement? SpanExperienceLocation(IWebElement? li) => li?.FindElement(SpanExperienceLocationXPath);

        private string ButtonExperienceSeeMoreXPath = ".//*/button[text()='…see more']";
        public IWebElement? ButtonExperienceSeeMore(IWebElement? li) => li?.FindElement(ButtonExperienceSeeMoreXPath);

        private string SpanExperienceJobDescriptionXPath = "./div/div[2]/div[2]/ul/li[{0}]/div/ul/li/div/div/div/div/span[1]";
        public IWebElement? SpanExperienceJobDescription(IWebElement? li) => li?.FindElement(
            string.Format(SpanExperienceJobDescriptionXPath,
                (IconExperienceLinkedinHelpedGetTheJob(li)?.Displayed ?? false) ? "2" : "1" ));

        private string SpanExperienceJobSkillsXPath = "./div/div[2]/div[2]/ul/li[{0}]/div/ul/li/div/div/div/div/span[1]";
        public IWebElement? SpanExperienceJobSkills(IWebElement? li) => li?.FindElement(
            string.Format(SpanExperienceJobSkillsXPath,
                (IconExperienceLinkedinHelpedGetTheJob(li)?.Displayed ?? false) ? "3" : "2"));

        private string IconExperienceLinkedinHelpedGetTheJobXPath = ".//*/li-icon";
        public IWebElement? IconExperienceLinkedinHelpedGetTheJob(IWebElement? li) => li?.FindElement(IconExperienceLinkedinHelpedGetTheJobXPath);


        public ProfilePage(Browser browser)
        {
            _browser = browser;
        }

        public Task EnsurePageLoaded()
        {
            throw new NotImplementedException();
        }

        public Task<Profile> GetProfileData()
        {
            Profile profile = new();

            profile.Name = H1Name?.Text;
            profile.SubTitle = DivSubTitle?.Text;
            profile.Location = SpanLocation?.Text;
            profile.About = SpanAbout?.Text;

            profile.ProfileExperiences = new List<ProfileExperience>();
            foreach (var li in Experiences)
            {
                // one for description
                ButtonExperienceSeeMore(li)?.Click();

                // second for skills
                ButtonExperienceSeeMore(li)?.Click();

                ProfileExperience pe = new();

                pe.Title = DivExperienceTitle(li)?.Text ?? String.Empty;
                pe.SubTitle = SpanExperienceSubTitle(li)?.Text ?? String.Empty;
                pe.Period = SpanExperiencePeriod(li)?.Text ?? String.Empty;
                pe.Description = SpanExperienceJobDescription(li)?.Text ?? String.Empty;
                pe.Skills = new List<string>();
                SpanExperienceJobSkills(li)?.Text.Split('·').ToList().ForEach(s => pe.Skills.Add(s.Trim()));
                pe.LinkedinHelpedGetJob = IconExperienceLinkedinHelpedGetTheJob(li)?.Displayed ?? false;

                profile.ProfileExperiences.Add(pe);
            }

            return Task.FromResult(profile);
        }

        public bool CurrentPage { get; }
    }
}
