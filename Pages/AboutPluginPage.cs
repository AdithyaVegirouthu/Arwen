using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arwendemo.Pages
{
    public class AboutPluginPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "HelpSectionInAppBody",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.About.Pages.AboutPage.ParagraphBodyText1"
            },
                {
                    "HearingAidCompatibilityLink",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.About.Pages.AboutPage.TertiaryButtonText1"

                },
                {
                    "UserGuidesLink",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.About.Pages.AboutPage.TertiaryButtonText2"
                },
                {
                    "HelpSectionInAppHeader",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.About.Pages.AboutPage.ParagraphSubHeaderText1"
                }
        };

        public AboutPluginPage(AndroidDriver driver)
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
            if (elementIds.TryGetValue(elementName, out string resourceId))
            {
                var elements = driver.FindElements(By.Id(resourceId));

                bool isDisplayed = elements.Any(element =>
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

                if (isDisplayed)
                    return true;
            }

          
            Console.WriteLine(
                "Using BasePage verification for: " + elementName);

            return base.IsDisplayed(elementName);
        }

    }
}
