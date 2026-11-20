using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class PairingCompletedMMPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "Body",
               "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.PairingCompletedMMPage.Body1"
            },
                {
                    "Done",
                  "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.PairingCompletedMMPage.PrimaryButtonText"
                },

        };

        public PairingCompletedMMPage(AndroidDriver driver)
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
                    "Resource ID not found for: " + elementName
                );
            }

            var elements = driver.FindElements(By.Id(resourceId));

            return elements.Any(element =>
            {
                try
                {
                    return element.Displayed;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}