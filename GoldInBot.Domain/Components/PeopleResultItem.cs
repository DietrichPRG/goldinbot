using GoldInBot.Domain.Helpers;
using OpenQA.Selenium;

namespace GoldInBot.Domain.Components
{
    public class PeopleResultItem
    {
        public PeopleResultItem(IWebElement element)
        {
            ConnectButton = element.FindElement("./div/div/div/div[3]/div/button/span[text()='Connect']/..");
            Name = element.FindElement("./div/div/div/div/div/div/div/span/span/a/span/span")!.Text;

            ProfileUrl = element.FindElement("./div/div/div/div/div/div/div/span/span/a")!.GetAttribute("href");

            var subTitlesLine = element.FindElements("./div/div/div/div/div/div[contains(@class, 'subtitle')]");

            if (subTitlesLine.Count > 2)
                subTitlesLine = subTitlesLine.Take(2).ToList();

            for (int i = 0; i < subTitlesLine.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        SubTitle = subTitlesLine[i].Text;
                        break;

                    case 1:
                        Address = subTitlesLine[i].Text;
                        break;
                }
            }
        }

        public string ProfileUrl { get; }

        public string Address { get; } = "";

        public string SubTitle { get; } = "";

        public string Name { get; }

        public IWebElement? ConnectButton { get; }

        public bool HasConnectButton => ConnectButton is not null;
    }
}
