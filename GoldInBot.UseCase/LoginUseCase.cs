using GoldInBot.Domain;
using GoldInBot.Domain.Pages;

namespace GoldInBot.UseCase
{
    public class LoginUseCase
    {
        private readonly string loginUrl = "https://linkedin.com/login";

        private readonly Browser _browser;
        private readonly LoginPage loginPage;

        public LoginUseCase(Browser browser)
        {
            this._browser = browser;
            this.loginPage = new LoginPage(this._browser);
            this._browser.driver.Navigate().GoToUrl(new Uri(this.loginUrl));
        }

        public Task Login(string userName, string passWord)
        {
            this.loginPage.WriteLoginInput(userName);
            this.loginPage.WritePasswordInput(passWord);
            this.loginPage.PressLoginButton();
        }
    }
}