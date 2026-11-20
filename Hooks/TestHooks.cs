
using ArwenAutomation.Support;
using OpenQA.Selenium.Appium.Android;
using Reqnroll;

namespace ArwenAutomation.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly ScenarioContext scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            AndroidDriver driver =
                DriverFactory.CreateDriver();

            scenarioContext["driver"] = driver;
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (scenarioContext.ContainsKey("driver"))
            {
                AndroidDriver driver =
                    (AndroidDriver)scenarioContext["driver"];

                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
