using OpenQA.Selenium;

namespace GoldInBot.Domain.Pages
{
    public class LoginPage
    {
        private readonly Browser _browser;

        private readonly string loginInputXPath = "//input[@Id='username']";

        private readonly string passwordInputXPath = "//input[@Id='password']";

        private readonly string loginButtonXPath = "//button[@aria-label='Sign in' or @aria-label='Entrar']";

        private IWebElement? loginInput => this._browser.driver.FindElement(By.XPath(this.loginInputXPath));
        private IWebElement? passwordInput => this._browser.driver.FindElement(By.XPath(this.passwordInputXPath));
        private IWebElement? loginButton => this._browser.driver.FindElement(By.XPath(this.loginButtonXPath));

        public LoginPage(Browser browser)
        {
            _browser = browser;
        }

        public void WriteLoginInput(string userName)
        {
            if (this.loginInput != null)
            {
                this.loginInput.SendKeys(userName);
            }
        }

        public void WritePasswordInput(string password)
        {
            if (this.passwordInput != null)
            {
                this.passwordInput.SendKeys(password);
            }
        }

        public void PressLoginButton()
        {
            if (this.loginButton != null)
            {
                this.loginButton.Click();
            }
        }
    }
}
