using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class SettingsPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "Sounddelay",
               "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.Title3"
            },
                {
                    "PasswordProtectedBroadcast",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.Title4"
                },
                {
                    "PasswordProtectedBroadcastBody",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.Body4"
                },
                {
                    "ShowPassword",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.24Eye"
                },
                {
                    "Close",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.Close"
                },
                {
                    "TVStreamerName",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.MenuItemLabelText1"
                },
                {
                    "TVStreamerType",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.MenuItemLabelText3"
                },
                {
                    "TVStreamerSerialNumber",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.MenuItemLabelText4"
                },
                {
                    "StreamAutomatically",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySettings.Pages.SettingsPage.Title1"
                }



        };

        public SettingsPage(AndroidDriver driver)
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