using TechTalk.SpecFlow;
using NUnit.Framework;
using BenefitsTA.UI.Pages;
using BenefitsTA.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BenefitsTA.UI.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions : UITests
    {
        private readonly LoginPage _loginPage;
        private readonly AppConfig _appConfig;

        public LoginStepDefinitions()
        {
            _loginPage = new LoginPage(Driver);
            _appConfig = new AppConfig();
        }

        [Given(@"an Employer")]
        public void GivenIAmOnTheLoginPage()
        {
            string url = _appConfig.GetServerName();

            Driver.Navigate().GoToUrl(url + "/Prod/Account/LogIn");

            // Use AppConfig to get the username and password from appsettings.json
            string username = _appConfig.GetUsername();
            string password = _appConfig.GetPassword();

            // Use the retrieved username and password to interact with the login form
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLoginButton();
        }
    }
}