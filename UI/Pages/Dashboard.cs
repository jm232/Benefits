using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Framework;


namespace BenefitsTA.UI.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;
        private WebDriverWait _wait;


        // Constructor - receives WebDriver from Step Definitions
        public DashboardPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = wait;
        }

        // Locators for elements (these can be XPaths, CSS Selectors, etc.)
        public string EmployeeId;
        public IWebElement AddEmployeeButton => _driver.FindElement(By.Id("add"));
        public IWebElement LogoutButton => _driver.FindElement(By.XPath("//a[contains(text(),'Log Out')]"));
        public IWebElement DeleteButton => _driver.FindElement(By.XPath("//td[8]/i[2]"));

        public string? GetCreatedEmployeeId(string name)
        {
            _wait.Until(_driver => _driver.FindElement(By.XPath("//td[contains(text(),'" + name + "')]")).Displayed);

            EmployeeId = _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + name +
                                      "']/preceding-sibling::td[2]")).Text;
            Console.WriteLine("id of employee is:" + EmployeeId);
            return EmployeeId;
        }

        public void ClickAddEmployeeButton()
        {
            AddEmployeeButton.Click(); // Click the AddEmployee button
        }

        public void ClickDeleteEmployeeButton(string employeeId)
        {
            _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[8]/i[2]")).Click();
        }

        public void ClickEditEmployeeButton(string employeeId)
        {
            _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[8]/i[1]")).Click();
        }

        public void AssertEmployeePresent(string employeeId, string firstname, string lastname, string dependents)
        {
            string EmployeeFirstName = _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[1]")).Text;
            string EmployeeLastName = _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[2]")).Text;
            string EmployeeDependents = _driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[3]")).Text;

            Assert.IsTrue(EmployeeFirstName.Contains(firstname), EmployeeFirstName + " not found");
            Assert.IsTrue(EmployeeLastName.Contains(lastname), EmployeeLastName + " not found");
            Assert.IsTrue(EmployeeDependents.Contains(dependents), EmployeeDependents + " not found");
        }

        public decimal GetEmployeesBenefitsCost(string employeeId)
        {
            return decimal.Parse(_driver
                .FindElement(By.XPath("//table[@id='employeesTable']//tr/td[text()='" + employeeId +
                                      "']/following-sibling::td[6]")).Text);
        }

        public void DashboardIsOpened()
        {
            _wait.Until(_driver => _driver.FindElement(By.XPath("//a[contains(text(),'Log Out')]")).Displayed);
        }
    }
}