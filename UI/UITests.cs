using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;

namespace BenefitsTA.UI
{
    public class UITests
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait;
        
        public UITests()
        {
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");  // Optional: Open browser maximized
            chromeOptions.AddArgument("--disable-extensions");  // Disable extensions for faster tests
            chromeOptions.AddArgument("--remote-debugging-port=9225");
            chromeOptions.AddArgument("--headless");
            Driver = new ChromeDriver("", chromeOptions);  // Initialize ChromeDriver with options
            
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [BeforeScenario]
        public void BeforeScenario(FeatureContext featureContext)
        {
            //TO DO: cleanup before start
        }
        
        [AfterScenario]
        public void AfterScenario(FeatureContext featureContext)
        {
        
            // Capture screenshot if the scenario failed
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot();
            }

            // TO DO: make cleanup of environment
            
            
            Driver.Quit();
        }

        private void TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var screenshotFilePath = Path.Combine(screenshotDirectory, $"screenshot_{timestamp}.png");

            screenshot.SaveAsFile(screenshotFilePath);

            // Log the path of the screenshot for debugging
            Console.WriteLine($"Screenshot saved at: {screenshotFilePath}");
        }
    }
}
