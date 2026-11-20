using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arwendemo.Pages
{
    public class TurnOnMMPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "Body",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.TurnOnMMPage.Body1"
                },
                {
                    "Close",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.TurnOnMMPage.Close"
                 },
                {
                    "Continue",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.TurnOnMMPage.PrimaryButtonText"
                }




            };

        public TurnOnMMPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public bool IsElementDisplayed(string elementName)
        {
            string resourceId = GetElementId(elementName);

            var elements = driver.FindElements(By.Id(resourceId));

            Console.WriteLine(
                elementName + " resource ID matches: " + elements.Count
            );

            foreach (var element in elements)
            {
                try
                {
                    Console.WriteLine(
                        "Text: " + element.Text +
                        ", Displayed: " + element.Displayed
                    );

                    if (element.Displayed)
                        return true;
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Element became stale: " + elementName);
                }
            }

            return false;
        }

        public string GetElementId(string elementName)
        {
            if (elementIds.TryGetValue(elementName, out string resourceId))
            {
                return resourceId;
            }

            throw new Exception("Unknown element: " + elementName);
        }
    }
}