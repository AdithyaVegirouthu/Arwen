using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class DeviceMenuPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
                {
                    "HIDeviceName",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.HIDeviceModel"
                },
               
                {
                    "ConnectionStatus",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.HearingInstrumentConnectionStatusMedia"
                },

            {
                "HI Settings",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.HISettingsMenuItemTitle"
            },
            {
                "DeviceNameTVStreamerPlus",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.DeviceNameTVPlus"
            },
            {
                    "TVStreamerPlusModel",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.TVPlusModel"
                },
                {
                    "TVStreamerPlusImage",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.TVPlusImage"
                },
                {
                    "TVPlusSettings",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.TVPlusSettingsMenuItemTitle"

                },
                {
                    "DeviceNameMultiMicPlus",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.DeviceNameMMPlus"
                },
                {
                    "MultiMicPlusModel",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.MMPlusModel"
                },
                {
                    "MultiMicPlusImage",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.MMPlusImage"
                },
                {
                    "MMPlusSettings",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.MMPlusSettingsMenuItemTitle"
                },
                {
                    "Pair new accessory",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.DeviceMenu.Pages.DeviceMenuPage.AccessoryPairingPrimaryButtonText"
                },





        };

        public DeviceMenuPage(AndroidDriver driver)
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
        public bool IsBatteryStatusDisplayed()
        {
            var elements = driver.FindElements(
                By.XPath("//android.widget.TextView[contains(@text,'%')]"));

            return elements.Any(element =>
            {
                try
                {
                    return element.Displayed &&
                           element.Text.Trim().EndsWith("%");
                }
                catch
                {
                    return false;
                }
            });
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