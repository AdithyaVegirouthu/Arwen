using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class HISettingsPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "LeftHearingAid_Model",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemTitle1"
                },
                {
                    "LeftHearingAidModelName",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemLabelText1"
                },
                {
                    "LeftHearingAid_SerialNumber",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemTitle2"
                },
                {
                    "LeftHearingAid_SerialNumberText",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemLabelText2"
                },
                {
                    "LeftHearingAid_FirmwareVersion",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemTitle3"
                },
                {
                    "LeftHearingAid_FWVersion",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.LeftHearingAidMenuItemLabelText3"
                },
                {
                    "RightHearingAid_Model",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemTitle1"
                },
                {
                    "RightHearingAid_ModelName",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemLabelText1"
                },
                {
                    "RightHearingAid_SerialNumber",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemTitle2"
                },
                {
                    "RightHearingAid_SerialNumberText",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemLabelText2"
                },
                {
                    "RightHearingAid_FirmwareVersion",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemTitle3"
                },
                {
                    "RightHearingAid_FWVersion",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.RightHearingAidMenuItemLabelText3"
                },
                {
                    "ReplaceHearingAids",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HISettings.Pages.HISettingsPage.OtherMenuItemTitle"
                }
            };

        public HISettingsPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public bool IsElementDisplayed(string elementName)
        {
            if (!elementIds.TryGetValue(elementName, out string elementId))
            {
                throw new System.ArgumentException(
                    "No resource ID found for: " + elementName);
            }

            return driver.FindElements(By.Id(elementId))
                .Any(element =>
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