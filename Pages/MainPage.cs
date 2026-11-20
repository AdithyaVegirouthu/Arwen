
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArwenAutomation.Pages
{
    public class MainPage : BasePage
    {
        private readonly AndroidDriver driver;

        private const string VolumeSliderId =
            "com.gnhearing.arwen.development:id/" +
            "ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage." +
            "HearingAidCombinedVolumeSlider";

        private const string VolumeLevelId =
            "com.gnhearing.arwen.development:id/" +
            "ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage." +
            "HearingAidCombinedVolumeLevel";
        private const string LeftvolumeLevelId =
       "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.HearingAidLeftVolumeLevel";

        private const string RightvolumeLevelId =
            "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.HearingAidRightVolumeLevel";

        private readonly Dictionary<string, string> elementIds =
            new Dictionary<string, string>
            {
               
                {
                    "HearingAidMuteVolume",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.TinnitusManagerActionToggleMute1"
                },
                {
                    "HearingAidCombinedVolumeSlider",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.HearingAidCombinedVolumeSlider"
                },
                {
                    "HearingAidSplitVolume",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.TinnitusManagerActionToggleSplit"
                },
                {
                    "Adjust your sound",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.AdjustYourSoundCardCTAHeaderText"
                },
                {
                    "ProgramBar",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorGroup"
                },
                {
                    "BeastMode",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorHearInNoiseText"
                },
                {
                    "HearInNoise",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorHearInNoiseText"
                },
                {
                    "Outdoor",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorOutdoorText"
                },
                {
                    "Music",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorMusicText"
                },
                {
                    "TVAndAudio",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.ProgramSelectorStreamingText"
                },
                {
                    "split",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.HearingAidActionToggleSplit"
                },
                {
                    "TinnitusSoundSettings",
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.HIControls.Pages.MainPage.TinnitusMenuItemTitle"
                }


            };

        public MainPage(AndroidDriver driver)
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
            if (!elementIds.TryGetValue(elementName, out string resourceId))
            {
                throw new Exception(
                    "Resource ID not found for: " + elementName);
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

        public void SetVolumeValue(int value)
        {
            SetSliderVolumeValue(VolumeLevelId, value, "Volume");
        }

        public void SetLeftVolumeValue(int value)
        {
            SetSliderVolumeValue(LeftvolumeLevelId, value, "Leftvolume");
        }

        public void SetRightVolumeValue(int value)
        {
            SetSliderVolumeValue(RightvolumeLevelId, value, "Rightvolume");
        }

        private void SetSliderVolumeValue(
            string volumeLevelId,
            int value,
            string sliderName)
        {
            if (value < -9 || value > 4)
            {
                throw new Exception(
                    sliderName + " must be between -9 and +4.");
            }

            IWebElement currentHandle = null;

            for (int i = -9; i <= 4; i++)
            {
                var elements = driver.FindElements(
                    By.Id(volumeLevelId + "[" + i + "]"));

                foreach (var element in elements)
                {
                    try
                    {
                        if (element.Displayed)
                        {
                            currentHandle = element;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }

                if (currentHandle != null)
                    break;
            }

            if (currentHandle == null)
            {
                throw new Exception(
                    "Current " + sliderName + " handle was not found.");
            }

            int startX =
                currentHandle.Location.X +
                (currentHandle.Size.Width / 2);

            int startY =
                currentHandle.Location.Y +
                (currentHandle.Size.Height / 2);

            double minX = 285;
            double maxX = 860;

            double targetX =
                minX + ((value + 9) / 13.0) * (maxX - minX);

            int endX = (int)Math.Round(targetX);

            Console.WriteLine(
                "Moving " + sliderName +
                " from X=" + startX +
                " to X=" + endX +
                ", Y=" + startY);

            var dragParameters =
                new Dictionary<string, object>
                {
            { "startX", startX },
            { "startY", startY },
            { "endX", endX },
            { "endY", startY },
            { "speed", 500 }
                };

            driver.ExecuteScript(
                "mobile: dragGesture",
                dragParameters);

            Console.WriteLine(
                sliderName + " slider drag completed for value: " + value);
        }


        public bool IsVolumeValueDisplayed(int value)
        {
            string resourceId =
                VolumeLevelId + "[" + value + "]";

            var elements = driver.FindElements(
                By.Id(resourceId));

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

        public bool IsLeftVolumeValueDisplayed(int value)
        {
            string resourceId =
                LeftvolumeLevelId + "[" + value + "]";

            Console.WriteLine("Verifying Left Volume: " + value);
            Console.WriteLine("Resource ID: " + resourceId);

            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10));

            try
            {
                return wait.Until(d =>
                {
                    var elements = d.FindElements(By.Id(resourceId));

                    Console.WriteLine(
                        "Matching Left Volume elements: " + elements.Count);

                    return elements.Any(element =>
                    {
                        try
                        {
                            return element.Displayed;
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                    });
                });
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine(
                    "Left Volume value " + value + " was not found.");

                return false;
            }
        }

        public bool IsRightVolumeValueDisplayed(int value)
        {
            string resourceId =
                RightvolumeLevelId + "[" + value + "]";

            var elements = driver.FindElements(
                By.Id(resourceId));

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
        public new void Tap(string elementName)
        {
            if (elementIds.TryGetValue(elementName, out string elementId))
            {
                driver.FindElement(By.Id(elementId)).Click();
                return;
            }

            base.Tap(elementName);
        }
    }
}
