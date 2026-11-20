using ArwenAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arwendemo.Pages
{
    public class AccessorySelectionPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "TV-Streamer+Header",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.CardLinkheader2"
                },

                {
                    "TV-Streamer+",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.TVFront"
                },

                {
                    "TVBody",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.CardLinkBodyText2"
                },
                {
                    "Pair TV-Streamer+",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.PrimaryButtonText2"
                },
                {
                    "Multi-Mic+",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.PrimaryButtonText1"
                },
                {
                    "Multi-Mic+Header",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.CardLinkheader1"
                },
                {
                     "MMBody",
                     "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AccessorySelectionMenu.Pages.AccessorySelectionPage.CardLinkBodyText1"
                }


            };

        public AccessorySelectionPage(AndroidDriver driver)
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