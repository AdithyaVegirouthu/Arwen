
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace ArwenAutomation.Pages
{
    public class EqualizerPresetPage : BasePage
    {
        private readonly AndroidDriver driver;

        private const string BassGridId =
            "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Equalizer.Pages.EqualizerPresetPage.BassGrid";

        private const string BassHandleId =
            "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Equalizer.Pages.EqualizerPresetPage.BassHandle";
        private readonly Dictionary<string, string> elementIds =
           new Dictionary<string, string>
       {
            {
                "-6",
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Equalizer.Pages.EqualizerPresetPage.BassGainValue[-6]"
            },
       };
        public EqualizerPresetPage(AndroidDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public void SetBassValue(int value)
        {
            if (value < -6 || value > 6)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Bass value must be between -6 and +6.");
            }

            IWebElement grid =
                driver.FindElement(By.Id(BassGridId));

            IWebElement handle =
                driver.FindElement(By.Id(BassHandleId));

            string gridBounds =
                grid.GetAttribute("bounds");

            string handleBounds =
                handle.GetAttribute("bounds");

            int[] gridCoordinates =
                GetBounds(gridBounds);

            int[] handleCoordinates =
                GetBounds(handleBounds);

            int centerX =
                (gridCoordinates[0] + gridCoordinates[2]) / 2;

            int currentY =
                (handleCoordinates[1] + handleCoordinates[3]) / 2;

            int handleHeight =
                handleCoordinates[3] - handleCoordinates[1];

            int minimumY =
                gridCoordinates[1] + handleHeight / 2;

            int maximumY =
                gridCoordinates[3] - handleHeight / 2;

            double percentage = (6 - value) / 12.0;

            int targetY = (int)Math.Round(
                minimumY +
                percentage * (maximumY - minimumY));

            Console.WriteLine(
                "Moving Bass from Y=" + currentY +
                " to Y=" + targetY);

            driver.ExecuteScript(
                "mobile: dragGesture",
                new Dictionary<string, object>
                {
                    ["startX"] = centerX,
                    ["startY"] = currentY,
                    ["endX"] = centerX,
                    ["endY"] = targetY,
                    ["speed"] = 500
                });

            Thread.Sleep(1000);

            Console.WriteLine(
                "Bass slider drag completed for value: " + value);
        }

        private int[] GetBounds(string bounds)
        {
            Match match = Regex.Match(
                bounds,
                @"\[(\d+),(\d+)\]\[(\d+),(\d+)\]");

            if (!match.Success)
            {
                throw new Exception(
                    "Unable to read element bounds: " + bounds);
            }

            return new int[]
            {
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value),
                int.Parse(match.Groups[4].Value)
            };
        }
    }
}
