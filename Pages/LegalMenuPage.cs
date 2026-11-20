using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace ArwenAutomation.Pages
{
    public class LegalMenuPage : BasePage
    {
        public LegalMenuPage(AndroidDriver driver)
            : base(driver)
        {
        }

        public bool IsTermsAndConditionsDisplayed()
        {
            var elements = driver.FindElements(
                By.XPath("//*[@text='Terms and conditions']")
            );

            return elements.Any(e => e.Displayed);
        }
    }
}