using GoldInBot.Domain;
using GoldInBot.Domain.Pages;

namespace GoldInBot.UseCase
{
    public class LoginUseCase
    {
        private const string LoginUrl = "https://linkedin.com/login";

        private readonly LoginPage _loginPage;

        public LoginUseCase(Browser browser)
        {
            this._loginPage = new LoginPage(browser);
            browser.driver.Navigate().GoToUrl(new Uri(LoginUrl));
        }

        public Task Login(string userName, string passWord)
        {
            this._loginPage.LoginInput?.SendKeys(userName);
            this._loginPage.PasswordInput?.SendKeys(passWord);
            this._loginPage.LoginButton?.Click();

            return Task.CompletedTask;
        }
    }
}