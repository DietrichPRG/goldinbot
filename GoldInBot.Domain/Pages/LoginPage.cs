using OpenQA.Selenium;
using GoldInBot.Domain.Helpers;

namespace GoldInBot.Domain.Pages
{
    public class LoginPage
    {
        private readonly Browser _browser;

        private const string LoginInputXPath = "//input[@Id='username']";

        private const string PasswordInputXPath = "//input[@Id='password']";

        private const string LoginButtonXPath = "//button[@aria-label='Sign in' or @aria-label='Entrar']";

        public IWebElement? LoginInput => this._browser.driver.FindElement(LoginInputXPath);
        public IWebElement? PasswordInput => this._browser.driver.FindElement(PasswordInputXPath);
        public IWebElement? LoginButton => this._browser.driver.FindElement(LoginButtonXPath);

        public LoginPage(Browser browser)
        {
            _browser = browser;
        }
    }
}
