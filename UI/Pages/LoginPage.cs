// LoginPage.cs (in TestAutomation.UI/Pages)
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BenefitsTA.UI.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Constructor - receives WebDriver from Step Definitions
        public LoginPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        // Locators for elements (these can be XPaths, CSS Selectors, etc.)
        private IWebElement UsernameField => _driver.FindElement(By.Id("Username"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("Password"));
        private IWebElement LoginButton => _driver.FindElement(By.XPath("//button[@type='submit']"));

        // Method to enter username
        public void EnterUsername(string username)
        {
            UsernameField.Clear();  // Clear the field first (in case there's any pre-filled data)
            UsernameField.SendKeys(username);  // Enter the username
        }

        // Method to enter password
        public void EnterPassword(string password)
        {
            PasswordField.Clear();  // Clear the field first
            PasswordField.SendKeys(password);  // Enter the password
        }

        // Method to click the login button
        public void ClickLoginButton()
        {
            LoginButton.Click();  // Click the login button
        }

      

    }
}