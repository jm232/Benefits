using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BenefitsTA.UI.Pages.Modals;

public class DeleteEmployee{
    private readonly IWebDriver _driver;
    private WebDriverWait _wait;

    public DeleteEmployee(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _wait = wait;

    }

    private IWebElement DeleteButton => _driver.FindElement(By.Id("deleteEmployee"));
    private IWebElement DeleteEmployeeHeader => _driver.FindElement(By.XPath("//h5[@class='modal-title']"));

    public void ClickDeleteButton(string lastname)
    {

        DeleteButton.Click();
        
        // TO DO: make sophisticated wait until element is not displayed
        if (DeleteButton.Displayed)
        {
            Thread.Sleep(2000);
        }
    }  
    public void DeleteModalIsOpened()
    {
        _wait.Until(_driver => _driver.FindElement(By.Id("deleteEmployee")).Displayed);

    }

    public bool NameIsNotDisplayed(string name)
    {
        bool elementIsPresent = false;
        try
        {
            elementIsPresent = _driver.FindElement(By.XPath("//td[text()='"+name+"']")).Displayed;

        }
        catch (Exception e)
        {
      
        }

        return elementIsPresent;
    }
}