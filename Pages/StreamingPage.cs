using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class StreamingPage : BasePage
    {
        private readonly AndroidDriver driver;

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
        {
            {
                "PublicAuracast",
              "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.StreamingAndAuracast.Pages.StreamingPage.Header2"
                },
                {
                    "StopAuracastStreaming",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.StreamingAndAuracast.Pages.StreamingPage.SecondaryButtonText"

                },
                {
                    "AccessoryDevice1",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.StreamingAndAuracast.Pages.StreamingPage.Header1"
                },
                {
                    "BondedStreamer1Body",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.StreamingAndAuracast.Pages.StreamingPage.Body1"
                },
                {
                    "Search for Auracast",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.StreamingAndAuracast.Pages.StreamingPage.SecondaryButtonText1"
                }
             

        };
        private readonly Dictionary<string, string> elementXpaths =
    new Dictionary<string, string>
    {
         {
             "Auracast Broadcast",
             "//android.widget.TextView[@text='Auracast™ Broadcasts']"
         }
    };
        public StreamingPage(AndroidDriver driver)
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
            string resourceId = null;
            string xpath = null;

            bool hasResourceId =
                elementIds.TryGetValue(elementName, out resourceId);

            bool hasXPath =
                elementXpaths.TryGetValue(elementName, out xpath);

            if (!hasResourceId && !hasXPath)
            {
                throw new Exception(
                    "Resource ID or XPath not found for: " + elementName);
            }

            Console.WriteLine("Verifying element: " + elementName);

            try
            {
                return wait.Until(d =>
                {
                    IEnumerable<IWebElement> elements;

                    if (hasResourceId)
                    {
                        elements = d.FindElements(By.Id(resourceId));
                    }
                    else
                    {
                        elements = d.FindElements(By.XPath(xpath));
                    }

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
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine(
                    "Element not found after waiting: " + elementName);

                return false;
            }
        }

    }
}
