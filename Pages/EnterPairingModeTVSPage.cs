using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arwendemo.Pages
{
    public class EnterPairingModeTVSPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "HI Settings",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.HISettingsMenuItemTitle"
            },
                {
                    "SearchForAccessory",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages.EnterPairingModeTVSPage.PrimaryButtonText"
                },
        };

        public EnterPairingModeTVSPage(AndroidDriver driver)
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
