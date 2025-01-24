using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BenefitsTA.UI.Pages.Modals;

public class EditEmployee{
    private readonly IWebDriver _driver;
    private WebDriverWait _wait;

    public EditEmployee(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _wait = wait;
    }

    private IWebElement FirstNameField => _driver.FindElement(By.Id("firstName"));
    private IWebElement LastNameField => _driver.FindElement(By.Id("lastName"));
    private IWebElement DependentsField => _driver.FindElement(By.Id("dependants"));
    private IWebElement UpdateButton => _driver.FindElement(By.Id("updateEmployee"));
    private IWebElement AddEmployeeHeader => _driver.FindElement(By.ClassName("modal-title"));

    public void EnterFirstName(string firstname)
    {
        FirstNameField.Clear();  
        FirstNameField.SendKeys(firstname);  
    }

    public void EnterLastName(string lastname)
    {
        LastNameField.Clear();  
        LastNameField.SendKeys(lastname);  
    }

    public void EnterDependents(string num)
    {
        DependentsField.Clear();  
        DependentsField.SendKeys(num);  
    }

    public void ClickUpdateButton()
    {

        UpdateButton.Click();  
    }  
    public string GetModalHeaderText()
    {
        return AddEmployeeHeader.Text;  
    }  
}