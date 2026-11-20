
using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Arwendemo.Pages
{
    public class StartupPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "PreparingBody",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Startup.Pages.StartupPage.LoadingText"
                },
               
            };

        public StartupPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public string GetElementId(string elementName)
        {
            if (elementIds.TryGetValue(elementName, out string resourceId))
            {
                return resourceId;
            }

            throw new Exception("Unknown element: " + elementName);
        }

        public bool IsElementDisplayed(string elementName)
        {
            if (!elementIds.TryGetValue(elementName, out string resourceId))
            {
                throw new Exception(
                    "Resource ID not found for: " + elementName);
            }

            try
            {
                var wait = new WebDriverWait(
                    driver,
                    TimeSpan.FromSeconds(20));

                return wait.Until(d =>
                {
                    var elements = d.FindElements(By.Id(resourceId));

                    foreach (var element in elements)
                    {
                        try
                        {
                            if (element.Displayed)
                            {
                                Console.WriteLine(
                                    elementName + " is displayed. ID: " +
                                    resourceId);

                                return true;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {

                        }
                    }

                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine(
                    elementName + " was not displayed within 20 seconds.");

                return false;
            }
        }
    }
}
