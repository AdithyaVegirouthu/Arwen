using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arwendemo.Pages
{
    public class AppPermissionsPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "Header",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AppPermissionsMenu.Pages.AppPermissionsPage.ParagraphHeaderText"
            },
                {
                    "BluetoothToggle",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AppPermissionsMenu.Pages.AppPermissionsPage.BluetoothToggleText"
                    
                },
                {
                    "NotificationToggle",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AppPermissionsMenu.Pages.AppPermissionsPage.NotificationToggleText"
                }
        };

        public AppPermissionsPage(AndroidDriver driver)
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
