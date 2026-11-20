using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ArwenAutomation.Pages
{
    public class TinnitusSoundsPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
                {
                    "CalmingWaves",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupSection2CalmingWaves."
                },
                {
                    "BreakingWaves",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupSection2BreakingWaves"
                },
                {
                    "ShoreLine",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupSection2Shoreline"
                },
                {
                    "BeachSurf",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupSection2BeachSurf"
                },
                {
                    "WaterCreek",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupSection2WaterCreek"
                },
                {
                    "Header",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupHeaderText"
                },
                {
                    "Body" ,
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TinnitusSoundSettings.Pages.TinnitusSoundsPage.RadioSelectorGroupBodyText"
                }
            };

        public TinnitusSoundsPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public string GetElementId(string elementName)
        {
            if (elementIds.TryGetValue(elementName, out string elementId))
            {
                return elementId;
            }

            return null;
        }

        public bool IsElementDisplayed(string elementName)
        {
            var screenSize = driver.Manage().Window.Size;
            for (int attempt = 1; attempt <= 8; attempt++)
            {
                bool isDisplayed = false;

                if (elementIds.TryGetValue(elementName, out string elementId))
                {
                    var elements = driver.FindElements(By.Id(elementId));

                    Console.WriteLine(
                        "Tinnitus check: " + elementName +
                        " | Attempt: " + attempt +
                        " | Elements found: " + elements.Count);

                    isDisplayed = elements.Any(element =>
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
                else
                {
                    var elements = driver.FindElements(
                        By.XPath(
                            "//*[@text=\"" + elementName +
                            "\" or @content-desc=\"" + elementName + "\"]"));

                    isDisplayed = elements.Any(element =>
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

                if (isDisplayed)
                {
                    Console.WriteLine(
                        elementName + " is displayed on TinnitusSoundsPage.");

                    return true;
                }

                if (attempt < 8)
                {
                    Console.WriteLine(
                        elementName +
                        " is not visible. Scrolling down. Attempt: " +
                        attempt);

                    driver.ExecuteScript(
                        "mobile: swipeGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 30,
                            ["top"] = (int)(screenSize.Height * 0.2),
                            ["width"] = screenSize.Width - 60,
                            ["height"] = (int)(screenSize.Height * 0.65),
                            ["direction"] = "up",
                            ["percent"] = 0.65
                        });

                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine(
                elementName +
                " was not found after checking and scrolling.");

            return false;
        }
    }
}