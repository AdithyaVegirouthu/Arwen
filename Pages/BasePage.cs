
using System;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
//Git
namespace ArwenAutomation.Pages
{
    public class BasePage
    {
        protected AndroidDriver driver;
        protected WebDriverWait wait;

        public BasePage(AndroidDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(20)
            );
        }
        public void Tap(string elementName)
        {


            Console.WriteLine("Searching for element: " + elementName);

            if (elementName.ToLower().Contains("hearing"))
            {
                TapHearingAidButton();
                return;
            }

            if (elementName == "Try Demo Mode")
            {
                TapTryDemoModeButton();
                return;
            }
            if (elementName == "SearchForAccessory")
            {
                string resourceId =
                    "com.gnhearing.arwen.development:id/" +
                    "ReSound.App.Legolas.Plugins.PairAccessoryWithPhone.Pages." +
                    "EnterPairingModeTVSPage.PrimaryButtonText";

                IWebElement element = wait.Until(d =>
                {
                    var elements = d.FindElements(By.Id(resourceId));

                    return elements.FirstOrDefault(e =>
                    {
                        try
                        {
                            return e.Displayed && e.Enabled;
                        }
                        catch
                        {
                            return false;
                        }
                    });
                });

                Console.WriteLine("SearchForAccessory button found.");

                if (element.GetAttribute("clickable") == "true")
                {
                    element.Click();
                }
                else
                {
                    TapElementByCoordinates(element);
                }

                Console.WriteLine("SearchForAccessory clicked successfully.");

                System.Threading.Thread.Sleep(3000);
                return;
            }

            if (elementName == "SeeAllTutorials")
            {
                IWebElement element = wait.Until(d =>
                    d.FindElement(By.Id(
                        "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HelpMenu.Pages.HelpMenuPage.SeeAllTutorialsPrimaryButtonText"
                    ))
                );

                TapElementByCoordinates(element);

                System.Threading.Thread.Sleep(3000);
                return;
            }


            if (elementName == "TinnitusSoundSettings")
            {
                string tinnitusId =
                    "com.gnhearing.arwen.development:id/" +
                    "ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.TinnitusMenuItemTitle";

                Console.WriteLine("Waiting for Tinnitus Sound Settings...");

                IWebElement element = wait.Until(d =>
                {
                    var elements = d.FindElements(By.Id(tinnitusId));

                    return elements.FirstOrDefault(e =>
                    {
                        try
                        {
                            return e.Displayed && e.Enabled;
                        }
                        catch
                        {
                            return false;
                        }
                    });
                });

                Console.WriteLine(
                    "Tinnitus found. Clickable: " +
                    element.GetAttribute("clickable"));

                try
                {
                    element.Click();
                }
                catch (WebDriverException)
                {
                    TapElementByCoordinates(element);
                }

                Console.WriteLine("Tinnitus Sound Settings clicked.");

                System.Threading.Thread.Sleep(2000);
                return;
            }



            if (elementName == "NoiseReduction" ||
                elementName == "WindNoiseReduction" ||
                elementName == "SpeechFocus" ||
                elementName == "Equalizer")
            {
                TapAdjustSoundOption(elementName);
                return;
            }

            try
            {
                IWebElement element = wait.Until(d =>
                {
                    var elements = d.FindElements(
                        By.XPath(
                            "//*[@text='" + elementName +
                            "' or @content-desc='" + elementName + "']"
                        )
                    );

                    return elements.FirstOrDefault(
                        e => e.Displayed && e.Enabled
                    );
                });

                Console.WriteLine(
                    elementName + " found successfully."
                );

                if (element.GetAttribute("clickable") == "true")
                {
                    element.Click();
                }
                else
                {
                    TapElementByCoordinates(element);
                }

                Console.WriteLine(
                    "Tap action performed: " + elementName
                );

                System.Threading.Thread.Sleep(3000);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine(
                    "ERROR: Element not found: " + elementName
                );

                throw;
            }
        }

        private void TapAdjustSoundOption(string elementName)
        {
            string resourceId = elementName switch
            {
                "NoiseReduction" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.NRRadioSelectorMenuItemTitle",

                "WindNoiseReduction" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.WNRRadioSelectorMenuItemTitle",

                "SpeechFocus" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.SFRadioSelectorMenuItemTitle",

                "Equalizer" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.EQPillSelectorMenuItemTitle",


                _ => throw new ArgumentException(
                    "Unknown option: " + elementName
                )
            };

            IWebElement selector = wait.Until(d =>
                d.FindElements(By.Id(resourceId))
                 .FirstOrDefault(e => e.Displayed)
            );

            TapElementByCoordinates(selector);

            Console.WriteLine(elementName + " selector tapped.");
        }


        private void TapHearingAidButton()
        {
            Stopwatch totalTime = Stopwatch.StartNew();

            Console.WriteLine("START: I don't have a hearing aid");

            string resourceId =
                "com.gnhearing.arwen.development:id/" +
                "ReSound.App.Legolas.Plugins.Welcome.Pages." +
                "WelcomeMessagePage.TertiaryButtonText";


            Stopwatch findTime = Stopwatch.StartNew();

            IWebElement buttonText = wait.Until(d =>
            {
                var elements = d.FindElements(By.Id(resourceId));

                return elements.FirstOrDefault(
                    e => e.Displayed && e.Enabled
                );
            });

            findTime.Stop();

            Console.WriteLine(
                "Element search time: " + findTime.Elapsed.TotalSeconds + " seconds"
            );


            Stopwatch tapTime = Stopwatch.StartNew();

            IWebElement clickableButton = null;

            try
            {
                clickableButton = buttonText.FindElement(
                    By.XPath(
                        "./ancestor-or-self::*[@clickable='true'][1]"
                    )
                );
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Clickable parent not found.");
            }

            if (clickableButton != null &&
                clickableButton.Displayed &&
                clickableButton.Enabled)
            {
                clickableButton.Click();
            }
            else
            {
                TapElementByCoordinates(buttonText);
            }

            tapTime.Stop();
            totalTime.Stop();

            Console.WriteLine(
                "Tap execution time: " + tapTime.Elapsed.TotalSeconds + " seconds"
            );

            Console.WriteLine(
                "TOTAL BUTTON METHOD TIME: " +
                totalTime.Elapsed.TotalSeconds + " seconds"
            );

        }

        private void TapTryDemoModeButton()
        {
            Console.WriteLine(
                "Finding all Try Demo Mode text elements..."
            );

            IWebElement bottomText = wait.Until(d =>
            {
                var elements = d.FindElements(
                    By.XPath("//*[@text='Try Demo Mode']")
                );

                var visibleElements = elements
                    .Where(e => e.Displayed && e.Enabled)
                    .OrderByDescending(e => e.Location.Y)
                    .ToList();

                Console.WriteLine(
                    "Matching visible elements found: " +
                    visibleElements.Count
                );

                return visibleElements.FirstOrDefault();
            });

            Console.WriteLine(
                "Bottom Try Demo Mode text found."
            );

            IWebElement clickableButton = null;

            try
            {
                clickableButton = bottomText.FindElement(
                    By.XPath(
                        "./ancestor-or-self::*[@clickable='true'][1]"
                    )
                );
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "Clickable parent not found. Using coordinates."
                );
            }

            if (clickableButton != null &&
                clickableButton.Displayed &&
                clickableButton.Enabled)
            {
                clickableButton.Click();
            }
            else
            {
                TapElementByCoordinates(bottomText);
            }

            Console.WriteLine("Demo Mode tap performed.");

            System.Threading.Thread.Sleep(5000);

            wait.Until(d =>
                d.FindElements(
                    By.XPath("//*[@text='Help' or @content-desc='Help']")
                ).Any(e => e.Displayed)
            );

            Console.WriteLine(
                "Demo Mode navigation successful. Help found."
            );
        }

        private void TapElementByCoordinates(IWebElement element)
        {
            int centerX =
                element.Location.X +
                element.Size.Width / 2;

            int centerY =
                element.Location.Y +
                element.Size.Height / 2;

            Console.WriteLine(
                "Tapping coordinates: X=" +
                centerX + ", Y=" + centerY
            );

            driver.ExecuteScript(
                "mobile: clickGesture",
                new Dictionary<string, object>
                {
                    ["x"] = centerX,
                    ["y"] = centerY
                }
            );

            Console.WriteLine(
                "Coordinate tap command executed."
            );
        }
        public void TapByResourceId(
    string resourceId,
    string elementName)
        {
            Console.WriteLine(
                "Searching resource ID for: " + elementName
            );

            IWebElement element = wait.Until(d =>
            {
                var elements = d.FindElements(By.Id(resourceId));

                return elements.FirstOrDefault(e =>
                {
                    try
                    {
                        return e.Displayed && e.Enabled;
                    }
                    catch
                    {
                        return false;
                    }
                });
            });

            Console.WriteLine(elementName + " found using resource ID.");

            if (element.GetAttribute("clickable") == "true")
                element.Click();
            else
                TapElementByCoordinates(element);

            Console.WriteLine(elementName + " clicked successfully.");
        }

        public virtual bool IsDisplayed(string elementName)
        {
            try
            {
                By locator;

                if (this is DeviceMenuPage deviceMenuPage)
                {
                    locator = By.Id(deviceMenuPage.GetElementId(elementName));
                }
                else
                {
                    locator = elementName switch
                    {
                        "TutorialsHeader" =>
                            By.XPath("//*[@text='Tutorials']"),

                        "SeeAllTutorials" =>
                            By.XPath("//*[@text='See all tutorials']"),

                        "NoiseReduction" =>
                            By.Id("com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.NoiseReductionParagraphSubHeaderText"),

                        "WindNoiseReduction" =>
                            By.Id("com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.WNRParagraphSubHeaderText"),

                        "SpeechFocus" =>
                            By.Id("com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.SpeechFocusParagraphSubHeaderText"),

                        "Equalizer" =>
                            By.Id("com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.AdjustHISoundMenu.Pages.AdjustHISoundMenuPage.EQParagraphSubHeaderText"),
                        "TinnitusSoundSettings" =>
By.Id("com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.TinnitusMenuItemTitle"),

                        _ => By.XPath(
                               "//*[@text='" + elementName +
                               "' or @content-desc='" + elementName +
                               "' or contains(translate(@resource-id," +
                               "'ABCDEFGHIJKLMNOPQRSTUVWXYZ'," +
                               "'abcdefghijklmnopqrstuvwxyz'),'" +
                               elementName.ToLowerInvariant() + "')]")
                    };
                }

                Console.WriteLine("Verifying: " + elementName);

                return wait.Until(d =>
                {
                    var elements = d.FindElements(locator);

                    return elements.Any(e =>
                    {
                        try
                        {
                            return e.Displayed;
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
                Console.WriteLine(elementName + " was not displayed.");
                return false;
            }
        }
    }
}
