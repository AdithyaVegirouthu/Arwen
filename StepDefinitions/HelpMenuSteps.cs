using ArwenAutomation.Pages;
using Arwendemo.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Reflection;

namespace ArwenAutomation.StepDefinitions
{
    [Binding]
    public class HelpMenuSteps
    {
        private readonly ScenarioContext scenarioContext;
        private AndroidDriver driver;

        public HelpMenuSteps(ScenarioContext scenarioContext)
        {
             driver = (AndroidDriver)scenarioContext["driver"];
            this.scenarioContext = scenarioContext;
        }

        private BasePage GetPage(string pageName)
        {
            return pageName switch
            {
                "WelcomePage" => new WelcomePage(driver),
                "DontHaveHIsPage" => new DontHaveHIsPage(driver),
                "MainTabBarPage" => new MainTabBarPage(driver),
                "HelpMenuPage" => new HelpMenuPage(driver),
                "TutorialsMenuPage" => new TutorialsMenuPage(driver),
                "CareAndMaintenancePage" => new CareAndMaintenancePage(driver),
                "ComfortAndFitPage" => new ComfortAndFitPage(driver),
                "MainPage" => new MainPage(driver),
                "AdjustHISoundMenuPage" => new AdjustHISoundMenuPage(driver),
                "NoiseReductionPage" => new NoiseReductionPage(driver),
                "WindNoiseReductionPage" => new WindNoiseReductionPage(driver),
                "SpeechFocusPage" => new SpeechFocusPage(driver),
                "EqualizerPresetPage" => new EqualizerPresetPage(driver),
                "AdjustEqualizerModalPage" => new AdjustEqualizerModalPage(driver),
                "MoreMenuPage" => new MoreMenuPage(driver),
                "ReadTermsAndConditionsPage" => new ReadTermsAndConditionsPage(driver),
                "LegalMenuPage" => new LegalMenuPage(driver),
                "ReadPrivacyPolicyPage" => new ReadPrivacyPolicyPage(driver),
                "DeviceMenuPage" => new DeviceMenuPage(driver),
                "HISettingsPage" => new HISettingsPage(driver),
                "AboutPluginPage" => new AboutPluginPage(driver),
                "AppPermissionsPage" => new AppPermissionsPage(driver),
                "StartupPage" => new StartupPage(driver),
                "TinnitusSoundsPage" => new TinnitusSoundsPage(driver),
                "AcceptDataConsentPage" => new AcceptDataConsentPage(driver),
                "AccessorySelectionPage" => new AccessorySelectionPage(driver),
                "TurnOnTVSPage" => new TurnOnTVSPage(driver),
                "EnterPairingModeTVSPage" => new EnterPairingModeTVSPage(driver),
                "FoundTVSPage" => new FoundTVSPage(driver),
                "PairingCompletedTVSPage" => new PairingCompletedTVSPage(driver),
                "SettingsPage" => new SettingsPage(driver),
                "TurnOnMMPage" => new TurnOnMMPage(driver),
                "EnterPairingModeMMPage" => new EnterPairingModeMMPage(driver),
                "FoundMMPage" => new FoundMMPage(driver),
                "PairingCompletedMMPage" => new PairingCompletedMMPage(driver),
                "StreamingPage" => new StreamingPage(driver),


                _ => throw new Exception("Page not found: " + pageName)
            };
        }

        [Given("I launch the app")]
        public void GivenILaunchTheApp()
        {
           

            Console.WriteLine("Clearing previous app data...");

            driver.ExecuteScript(
                "mobile: clearApp",
                new Dictionary<string, object>
                {
                    ["appId"] = "com.gnhearing.arwen.development"
                }
            );

            Console.WriteLine("Previous app data cleared.");

            driver.ActivateApp("com.gnhearing.arwen.development");

            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(30)
            );

            wait.Until(d =>
                d.FindElements(By.Id(
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Welcome.Pages.WelcomeMessagePage.TertiaryButtonText"
                )).Any(e => e.Displayed)
            );

            Console.WriteLine("Welcome Page displayed.");
        }

        [Given(@"I press '(.*)' on '(.*)'")]
         [When(@"I press '(.*)' on '(.*)'")]
        public void WhenIPressOnPage(string elementName, string pageName)
        {
            Console.WriteLine("Clicking: " + elementName + " on " + pageName);

            BasePage page = GetPage(pageName);

            if (pageName == "WelcomePage" &&
                elementName.Equals("I dont have a hearing aids",
                    StringComparison.OrdinalIgnoreCase))
            {
                driver.FindElement(By.Id(
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.Welcome.Pages.WelcomeMessagePage.TertiaryButtonText"
                )).Click();
            }
            else if (TryGetPageElementId(page, elementName, out string resourceId))
            {
                page.TapByResourceId(resourceId, elementName);
            }
            else
            {
                page.Tap(elementName);
            }

            Console.WriteLine(elementName + " clicked successfully.");
        }
        private bool TryGetPageElementId(
    BasePage page,
    string elementName,
    out string resourceId)
        {
            resourceId = null;

            var method = page.GetType().GetMethod(
                "GetElementId",
                new[] { typeof(string) }
            );

            if (method == null)
                return false;

            try
            {
                resourceId = method.Invoke(
                    page,
                    new object[] { elementName }
                ) as string;

                return !string.IsNullOrWhiteSpace(resourceId);
            }
            catch (TargetInvocationException)
            {
                return false;
            }
        }
        private void TapSpeechFocusLevel(string direction, string level)
        {
            Console.WriteLine(
                "Searching for Speech Focus level: " + level
            );

            bool elementFound = false;

            for (int i = 0; i < 6; i++)
            {
                var elements = driver.FindElements(
                    By.XPath("//*[@text='" + level + "']")
                );

                IWebElement option = elements.FirstOrDefault(e =>
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

                if (option != null)
                {
                    Console.WriteLine(level + " found.");

                    int centerX =
                        option.Location.X + option.Size.Width / 2;

                    int centerY =
                        option.Location.Y + option.Size.Height / 2;

                    driver.ExecuteScript(
                        "mobile: clickGesture",
                        new Dictionary<string, object>
                        {
                            ["x"] = centerX,
                            ["y"] = centerY
                        }
                    );

                    Console.WriteLine(
                        "Speech Focus level selected: " + level
                    );

                    elementFound = true;
                    break;
                }

                Console.WriteLine(
                    level + " not visible. Scroll attempt: " + (i + 1)
                );

                var screenSize = driver.Manage().Window.Size;

                driver.ExecuteScript(
                    "mobile: scrollGesture",
                    new Dictionary<string, object>
                    {
                        ["left"] = 0,
                        ["top"] = (int)(screenSize.Height * 0.2),
                        ["width"] = screenSize.Width,
                        ["height"] = (int)(screenSize.Height * 0.65),
                        ["direction"] = direction.ToLower(),
                        ["percent"] = 0.7
                    }
                );

                System.Threading.Thread.Sleep(500);
            }

            if (!elementFound)
            {
                throw new Exception(
                    "Speech Focus option not found: " + level
                );
            }
        }

        [Then(@"verify '(.*)' is '(.*)' on '(.*)'")]
        public void ThenVerifyElementIsDisplayed(
     string elementName,
     string expectedStatus,
     string pageName)
        {
            string selectedValue = null;

            if ((pageName == "MainPage" &&
               (elementName.StartsWith("Volume' as '") ||
                elementName.StartsWith("Leftvolume' as '") ||
               elementName.StartsWith("Rightvolume' as '"))) ||
               (pageName == "EqualizerPresetPage" &&
                elementName.StartsWith("Bass' as '")))
            {
                string[] parts = elementName.Split(
                    new string[] { "' as '" },
                    StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    elementName = parts[0];
                    selectedValue = parts[1];
                }
            }
           
            if (expectedStatus == "displayed")
            {

                bool actualStatus;
                if (pageName == "MainPage" &&
                elementName.Equals("Volume", StringComparison.OrdinalIgnoreCase))
                {
                    if (!int.TryParse(selectedValue, out int volumeValue))
                    {
                        throw new Exception(
                            "Invalid Volume verification value: " + selectedValue);
                    }

                    actualStatus =
                        new MainPage(driver).IsVolumeValueDisplayed(volumeValue);
                    Console.WriteLine(
                  $"Expected Value: {volumeValue}, Actual Status: {actualStatus}");
                }
                else if (pageName == "MainPage" &&
                         elementName.Equals("Leftvolume", StringComparison.OrdinalIgnoreCase))
                {
                    if (!int.TryParse(selectedValue, out int volumeValue))
                    {
                        throw new Exception(
                            "Invalid Left Volume verification value: " + selectedValue);
                    }

                    actualStatus =
                        new MainPage(driver).IsLeftVolumeValueDisplayed(volumeValue);
                    Console.WriteLine(
                  $"Expected Value: {volumeValue}, Actual Status: {actualStatus}");
                }
                else if (pageName == "MainPage" &&
                         elementName.Equals("Rightvolume", StringComparison.OrdinalIgnoreCase))
                {
                    if (!int.TryParse(selectedValue, out int volumeValue))
                    {
                        throw new Exception(
                            "Invalid Right Volume verification value: " + selectedValue);
                    }

                    actualStatus =
                        new MainPage(driver).IsRightVolumeValueDisplayed(volumeValue);
                }

                else if (pageName == "EqualizerPresetPage" &&
                         elementName == "Bass")
                {
                    if (!int.TryParse(selectedValue, out int value))
                    {
                        throw new Exception(
                            "Invalid Bass verification value: " + selectedValue);
                    }

                    string bassId =
                        "com.gnhearing.arwen.development:id/" +
                        "ReSound.App.Legolas.Plugins.Equalizer.Pages." +
                        "EqualizerPresetPage.BassGainValue[" +
                        value + "]";

                    actualStatus = driver.FindElements(By.Id(bassId))
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

                    Console.WriteLine(
                        $"Bass Expected Value: {value}, Actual Status: {actualStatus}");
                }

                else if (pageName == "LegalMenuPage" &&
                         elementName == "TermsAndConditions")
                {
                    actualStatus =
                        new LegalMenuPage(driver)
                            .IsTermsAndConditionsDisplayed();
                }

                else if (pageName == "HISettingsPage" &&
                         elementName != "Settings" &&
                         elementName != "Header" &&
                         elementName != "Name" &&
                         elementName != "HINAME")
                {
                    actualStatus =
                        new HISettingsPage(driver)
                            .IsElementDisplayed(elementName);
                }
                else if (pageName == "DeviceMenuPage" &&
                      (elementName == "LeftHIBatteryStatus" ||
                      elementName == "RightHIBatteryStatus"))
                {
                    actualStatus =
                        new DeviceMenuPage(driver).IsBatteryStatusDisplayed();
                }
                else if (pageName == "AcceptDataConsentPage")
                {
                    actualStatus =
                        new Arwendemo.Pages.AcceptDataConsentPage(driver)
                            .IsElementDisplayed(elementName);
                }
                else if (pageName == "StreamingPage")
                {
                    actualStatus =
                        new StreamingPage(driver)
                            .IsElementDisplayed(elementName);
                }
                else
                {
                    actualStatus =
                        IsElementDisplayedOnPage(pageName, elementName);
                }
                if (!actualStatus)
                {
                    if (pageName == "MainPage" &&
                        (elementName.Equals("Volume", StringComparison.OrdinalIgnoreCase) ||
                         elementName.Equals("Leftvolume", StringComparison.OrdinalIgnoreCase) ||
                         elementName.Equals("Rightvolume", StringComparison.OrdinalIgnoreCase)))
                    {
                        string volumeType = elementName.Equals("Leftvolume", StringComparison.OrdinalIgnoreCase)
                            ? "Left"
                            : elementName.Equals("Rightvolume", StringComparison.OrdinalIgnoreCase)
                                ? "Right"
                                : "";

                        string resourceName = volumeType == ""
                            ? "HearingAidVolumeLevel"
                            : "HearingAid" + volumeType + "VolumeLevel";

                        string actualValue = "Not found";

                        var elements = driver.FindElements(By.XPath(
                            $"//*[contains(@resource-id,'{resourceName}')]"));

                        foreach (var element in elements)
                        {
                            try
                            {
                                if (element.Displayed)
                                {
                                    string id = element.GetAttribute("resource-id");
                                    int start = id.LastIndexOf('[');
                                    int end = id.LastIndexOf(']');

                                    if (start >= 0 && end > start)
                                        actualValue = id.Substring(start + 1, end - start - 1);

                                    break;
                                }
                            }
                            catch { }
                        }

                        throw new Exception(
                            $"Expected Value: {selectedValue}\n" +
                            $"Actual Value: {actualValue}\n" +
                            "Actual Status: FAIL");
                    }

                    NUnit.Framework.Assert.Fail(
                        elementName +
                    (pageName == "EqualizerPresetPage" &&
 elementName == "Bass" ? " value " + selectedValue : "") +
                        " is not displayed on " + pageName);
                }

                Console.WriteLine(elementName + " is displayed.");


            }
            else if (expectedStatus == "checked")
            {
                if (pageName != "NoiseReductionPage" &&
                    pageName != "SpeechFocusPage")
                {
                    throw new Exception(
                        "Checked verification is currently supported only for NoiseReductionPage and SpeechFocusPage.");
                }

                bool isChecked =
                    IsNoiseReductionLevelChecked(elementName);

                NUnit.Framework.Assert.That(
                    isChecked,
                    NUnit.Framework.Is.True,
                    elementName + " is not checked on " + pageName);

                Console.WriteLine(elementName + " is checked.");
            }
            else if (expectedStatus.Equals(
           "Not displayed",
           StringComparison.OrdinalIgnoreCase))
            {
                bool isDisplayed =
                    IsElementDisplayedOnPage(pageName, elementName);

                NUnit.Framework.Assert.That(
                    isDisplayed,
                    NUnit.Framework.Is.False,
                    elementName + " is still displayed on " + pageName);

                Console.WriteLine(elementName + " is not displayed.");
            }
            else
            {
                NUnit.Framework.Assert.Fail(
                    "Unsupported expected status: " + expectedStatus);
            }
        }
        private bool IsNoiseReductionLevelChecked(string level)
        {
            Console.WriteLine(
                "Checking selected state for: " + level
            );

            XDocument document = XDocument.Parse(driver.PageSource);

            var matchingNodes = document
                .Descendants()
                .Where(node =>
                    (string)node.Attribute("text") == level
                );

            foreach (var node in matchingNodes)
            {
                foreach (var element in node.AncestorsAndSelf().Take(5))
                {
                    string checkedValue =
                        (string)element.Attribute("checked");

                    string selectedValue =
                        (string)element.Attribute("selected");

                    string contentDescription =
                        (string)element.Attribute("content-desc");

                    if (string.Equals(
                        checkedValue,
                        "true",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    if (string.Equals(
                        selectedValue,
                        "true",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(contentDescription) &&
                        (contentDescription.Contains(
                            "selected",
                            StringComparison.OrdinalIgnoreCase) ||
                         contentDescription.Contains(
                            "checked",
                            StringComparison.OrdinalIgnoreCase)))
                    {
                        return true;
                    }
                }
            }

            Console.WriteLine(
                "No checked/selected attribute found for: " + level
            );

            return false;
        }
        [When("I press Android Back")]
        public void WhenIPressAndroidBack()
        {
            Console.WriteLine(
                "Clicking Close button on Care and Maintenance page..."
            );

            string closeButtonId =
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.ArticleTutorialsCareAndMaintenance.Pages.CareAndMaintenancePage.Close";

            WebDriverWait wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(20)
            );

            var closeButton = wait.Until(d =>
            {
                var elements = d.FindElements(By.Id(closeButtonId));

                return elements.Count > 0 && elements[0].Displayed
                    ? elements[0]
                    : null;
            });

            closeButton.Click();

            Console.WriteLine("Close button clicked successfully.");

            string tutorialsPageTitleId =
                "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TutorialsMenu.Pages.TutorialsMenuPage.PageTitle";

            wait.Until(d =>
                d.FindElements(By.Id(tutorialsPageTitleId))
                    .Any(e => e.Displayed)
            );

            Console.WriteLine(
                "Returned to Tutorials Menu successfully."
            );

        }

        [Given("I scroll {string} and press {string} on {string}")]
        [When("I scroll {string} and press {string} on {string}")]

        public void WhenIScrollAndPressOn(
    string direction,
    string elementName,
    string pageName)
        {
            Console.WriteLine(
                "Searching for " + elementName + " on " + pageName
            );

            if (pageName == "DeviceMenuPage")
            {
                Console.WriteLine("Searching Device menu element: " + elementName);

                DeviceMenuPage devicePage = new DeviceMenuPage(driver);

                string resourceId = devicePage.GetElementId(elementName);

                var screenSize = driver.Manage().Window.Size;

                bool clicked = false;

                for (int attempt = 1; attempt <= 8; attempt++)
                {
                    Console.WriteLine("Device menu search attempt: " + attempt);

                    var elements = driver.FindElements(By.Id(resourceId));

                    var target = elements.FirstOrDefault(e =>
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

                    if (target != null)
                    {
                        Console.WriteLine(elementName + " found.");

                        int centerX = target.Rect.X + target.Rect.Width / 2;
                        int centerY = target.Rect.Y + target.Rect.Height / 2;

                        driver.ExecuteScript(
                            "mobile: clickGesture",
                            new Dictionary<string, object>
                            {
                                ["x"] = centerX,
                                ["y"] = centerY
                            }
                        );

                        Console.WriteLine(elementName + " clicked.");

                        Thread.Sleep(3000);

                        clicked = true;
                        break;
                    }

                    Console.WriteLine("Not found. Scrolling down...");

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
                        }
                    );

                    Thread.Sleep(1500);
                }

                NUnit.Framework.Assert.That(
                    clicked,
                    NUnit.Framework.Is.True,
                    elementName + " was not found after scrolling on DeviceMenuPage."
                );

                return;
            }
            if (pageName == "AccessorySelectionPage")
            {
                Console.WriteLine(
                    "Searching Accessory Selection element: " + elementName
                );

                Arwendemo.Pages.AccessorySelectionPage accessoryPage =
                    new Arwendemo.Pages.AccessorySelectionPage(driver);

                string resourceId = accessoryPage.GetElementId(elementName);

                var screenSize = driver.Manage().Window.Size;

                bool clicked = false;

                for (int attempt = 1; attempt <= 8; attempt++)
                {
                    Console.WriteLine(
                        "Accessory search attempt: " + attempt
                    );

                    var elements = driver.FindElements(By.Id(resourceId));

                    var target = elements.FirstOrDefault(e =>
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

                    if (target != null)
                    {
                        Console.WriteLine(elementName + " found.");

                        int centerX = target.Rect.X + target.Rect.Width / 2;
                        int centerY = target.Rect.Y + target.Rect.Height / 2;

                        driver.ExecuteScript(
                            "mobile: clickGesture",
                            new Dictionary<string, object>
                            {
                                ["x"] = centerX,
                                ["y"] = centerY
                            }
                        );

                        Console.WriteLine(elementName + " clicked.");

                        Thread.Sleep(3000);

                        clicked = true;
                        break;
                    }

                    Console.WriteLine(
                        "Element not found. Scrolling down..."
                    );

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
                        }
                    );

                    Thread.Sleep(1500);
                }

                NUnit.Framework.Assert.That(
                    clicked,
                    NUnit.Framework.Is.True,
                    elementName +
                    " was not found after scrolling on AccessorySelectionPage."
                );

                return;
            }

            if (pageName == "NoiseReductionPage")
            {
                TapNoiseReductionLevel(direction, elementName);
                return;
            }

            if (pageName == "SpeechFocusPage")
            {
                TapSpeechFocusLevel(direction, elementName);
                return;
            }
            if (pageName == "MainPage")
            {
                Console.WriteLine(
                    "Scrolling " + direction + " on MainPage to find " + elementName
                );

                MainPage mainPage = new MainPage(driver);

                string resourceId = mainPage.GetElementId(elementName);

                if (string.IsNullOrEmpty(resourceId))
                {
                    throw new Exception(
                        "Resource ID not found for " + elementName + " on MainPage."
                    );
                }

                var screenSize = driver.Manage().Window.Size;

                string swipeDirection;

                if (direction == "Up")
                {
                    swipeDirection = "down";
                }
                else if (direction == "Down")
                {
                    swipeDirection = "up";
                }
                else
                {
                    throw new Exception(
                        "Unsupported scroll direction: " + direction
                    );
                }

                bool clicked = false;

                for (int attempt = 1; attempt <= 8; attempt++)
                {
                    Console.WriteLine(
                        "MainPage " + direction + " swipe attempt: " + attempt
                    );

                    driver.ExecuteScript(
                        "mobile: swipeGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 30,
                            ["top"] = (int)(screenSize.Height * 0.2),
                            ["width"] = screenSize.Width - 60,
                            ["height"] = (int)(screenSize.Height * 0.65),
                            ["direction"] = swipeDirection,
                            ["percent"] = 0.65
                        }
                    );

                    Thread.Sleep(1200);

                    var elements = driver.FindElements(By.Id(resourceId));

                    var target = elements.FirstOrDefault(element =>
                    {
                        try
                        {
                            return element.Displayed && element.Enabled;
                        }
                        catch
                        {
                            return false;
                        }
                    });

                    if (target != null)
                    {
                        Console.WriteLine(
                            elementName + " found after scrolling."
                        );

                        target.Click();

                        Console.WriteLine(
                            elementName + " clicked successfully."
                        );

                        clicked = true;
                        break;
                    }

                    Console.WriteLine(
                        elementName + " not visible after swipe attempt " + attempt
                    );
                }

                NUnit.Framework.Assert.That(
                    clicked,
                    NUnit.Framework.Is.True,
                    elementName +
                    " was not found after scrolling " + direction + " on MainPage."
                );

                Thread.Sleep(2000);
                return;
            }
            if (pageName == "MainPage" &&
               elementName == "TinnitusSoundSettings")
            {
                Console.WriteLine("Searching for Tinnitus Sound Settings...");
                MainPage mainPage = new MainPage(driver);

                string tinnitusId =
                    mainPage.GetElementId("TinnitusSoundSettings");

                bool clicked = false;

                int screenWidth = driver.Manage().Window.Size.Width;
                int screenHeight = driver.Manage().Window.Size.Height;

                for (int attempt = 1; attempt <= 8; attempt++)
                {
                    Console.WriteLine("Tinnitus search attempt: " + attempt);

                    var elements = driver.FindElements(By.Id(tinnitusId));
                    

                    var target = elements.FirstOrDefault(e =>
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



                    Console.WriteLine("Not found. Scrolling down...");

                    driver.ExecuteScript(
                        "mobile: swipeGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 30,
                            ["top"] = screenHeight / 5,
                            ["width"] = screenWidth - 60,
                            ["height"] = (int)(screenHeight * 0.65),
                            ["direction"] = "up",
                            ["percent"] = 0.75
                        }
                    );

                    Thread.Sleep(1500);
                }

                NUnit.Framework.Assert.That(
                    clicked,
                    NUnit.Framework.Is.True,
                    "Tinnitus Sound Settings was not found after scrolling."
                );

                return;
            }
            

           

            if (elementName == "Adjust your sound")
            {
                Console.WriteLine("Starting scroll on MainPage...");

                bool adjustSoundClicked = false;

                int screenWidth =
                    driver.Manage().Window.Size.Width;

                int screenHeight =
                    driver.Manage().Window.Size.Height;

                Console.WriteLine(
                    $"Screen size: {screenWidth} x {screenHeight}"
                );

                int left = 30;
                int top = screenHeight / 5;
                int width = screenWidth - 60;
                int height = (int)(screenHeight * 0.65);

                for (int i = 1; i <= 8; i++)
                {
                    Console.WriteLine("Search attempt: " + i);

                    var elements = driver.FindElements(
                        By.XPath(
                            "//*[contains(@text,'Adjust your sound') or " +
                            "contains(@content-desc,'Adjust your sound')]"
                        )
                    );

                    var target = elements.FirstOrDefault(e =>
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

                    if (target != null)
                    {
                        Console.WriteLine("Adjust your sound found.");

                        int centerX =
                            target.Rect.X + target.Rect.Width / 2;

                        int centerY =
                            target.Rect.Y + target.Rect.Height / 2;

                        driver.ExecuteScript(
                            "mobile: clickGesture",
                            new Dictionary<string, object>
                            {
                                ["x"] = centerX,
                                ["y"] = centerY
                            }
                        );

                        Console.WriteLine("Adjust your sound clicked.");

                        Thread.Sleep(3000);

                    }

                    Console.WriteLine(
                        "Not found. Swiping UP to reveal lower content..."
                    );

                    driver.ExecuteScript(
                        "mobile: swipeGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = left,
                            ["top"] = top,
                            ["width"] = width,
                            ["height"] = height,
                            ["direction"] = "up",
                            ["percent"] = 0.75
                        }
                    );

                    Thread.Sleep(2000);
                }


                NUnit.Framework.Assert.That(
                    adjustSoundClicked,
                    NUnit.Framework.Is.True,
                    "Adjust your sound was not found after scrolling."
                );


            }

            bool isClicked = false;

            string elementId = elementName switch
            {
                "Care and maintenance" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TutorialsMenu.Pages.TutorialsMenuPage.CareAndMaintenanceMenuItemTitle",

                "Comfort and fit" =>
                    "com.gnhearing.arwen.development:id/ReSound.App.Legolas.Plugins.TutorialsMenu.Pages.TutorialsMenuPage.ComfortAndFitMenuItemTitle",

                _ => throw new Exception(
                    "Unknown element: " + elementName
                )
            };

            for (int i = 0; i < 6; i++)
            {
                var elements = driver.FindElements(By.Id(elementId));

                Console.WriteLine(
                    "Attempt: " + (i + 1) +
                    ", Found: " + elements.Count
                );

                if (elements.Count > 0 && elements[0].Displayed)
                {
                    int centerX =
                        elements[0].Rect.X +
                        elements[0].Rect.Width / 2;

                    int centerY =
                        elements[0].Rect.Y +
                        elements[0].Rect.Height / 2;

                    driver.ExecuteScript(
                        "mobile: clickGesture",
                        new Dictionary<string, object>
                        {
                            ["x"] = centerX,
                            ["y"] = centerY
                        }
                    );

                    Console.WriteLine(
                        elementName + " tapped successfully."
                    );

                    isClicked = true;
                    break;
                }

                if (direction == "Down")
                {
                    driver.ExecuteScript(
                        "mobile: scrollGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 100,
                            ["top"] = 300,
                            ["width"] = 880,
                            ["height"] = 1600,
                            ["direction"] = "down",
                            ["percent"] = 0.50
                        }
                    );

                    Thread.Sleep(1000);
                }
                else
                {
                    throw new Exception(
                        "Unsupported scroll direction: " + direction
                    );
                }
            }

            NUnit.Framework.Assert.That(
                isClicked,
                NUnit.Framework.Is.True,
                elementName +
                " was not found after scrolling on " + pageName
            );


        }

        private void TapNoiseReductionLevel(
            string direction,
            string level)
        {
            Console.WriteLine(
                "Finding Noise Reduction level: " + level
            );

            bool isClicked = false;

            for (int i = 1; i <= 6; i++)
            {
                var elements = driver.FindElements(
                    By.XPath("//*[@text='" + level + "']")
                );

                var target = elements.FirstOrDefault(e =>
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

                Console.WriteLine(
                    "Attempt: " + i + ", Found: " + elements.Count
                );

                if (target != null)
                {
                    int centerX =
                        target.Rect.X + target.Rect.Width / 2;

                    int centerY =
                        target.Rect.Y + target.Rect.Height / 2;

                    driver.ExecuteScript(
                        "mobile: clickGesture",
                        new Dictionary<string, object>
                        {
                            ["x"] = centerX,
                            ["y"] = centerY
                        }
                    );

                    Console.WriteLine(
                        level + " tapped successfully."
                    );

                    isClicked = true;
                    break;
                }

                if (direction == "Down")
                {
                    driver.ExecuteScript(
                        "mobile: scrollGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 100,
                            ["top"] = 300,
                            ["width"] = 880,
                            ["height"] = 1600,
                            ["direction"] = "down",
                            ["percent"] = 0.50
                        }
                    );

                    Thread.Sleep(1000);
                }
                else
                {
                    throw new Exception(
                        "Unsupported scroll direction: " + direction
                    );
                }
            }

            NUnit.Framework.Assert.That(
                isClicked,
                NUnit.Framework.Is.True,
                level + " was not found on NoiseReductionPage."
            );

            Thread.Sleep(2000);
        }

        [Then("I scroll {string} and verify {string} is {string} on {string}")]


        public void ThenIScrollAndVerifyIsOn(
    string direction,
    string elementName,
    string verificationType,
    string pageName)
        {
            Console.WriteLine(
                "Scrolling " + direction +
                " to verify " + elementName +
                " on " + pageName);

            if (!verificationType.Equals(
                "displayed",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Unsupported verification type: " + verificationType);
            }

            if (!direction.Equals("Down", StringComparison.OrdinalIgnoreCase) &&
                !direction.Equals("Up", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Unsupported scroll direction: " + direction);
            }

            var screenSize = driver.Manage().Window.Size;
            BasePage page = GetPage(pageName);

            string swipeDirection =
                direction.Equals("Down", StringComparison.OrdinalIgnoreCase)
                ? "down"
                : "up";

            for (int i = 0; i < 8; i++)
            {
                bool elementFound = false;

                if (TryGetPageElementId(
                    page,
                    elementName,
                    out string resourceId))
                {
                    Console.WriteLine(
                        "Searching resource ID: " + resourceId);

                    var elements = driver.FindElements(By.Id(resourceId));

                    elementFound = elements.Any(e =>
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
                }
                else if (pageName == "AccessorySelectionPage")
                {
                    elementFound =
                        new AccessorySelectionPage(driver)
                            .IsElementDisplayed(elementName);
                }
                else if (pageName == "DeviceMenuPage")
                {
                    elementFound =
                        new DeviceMenuPage(driver)
                            .IsElementDisplayed(elementName);
                }
                else
                {
                    Console.WriteLine(
                        "Searching using XPath: " + elementName);

                    var elements = driver.FindElements(
                        By.XPath(
                            "//*[@text='" + elementName +
                            "' or @content-desc='" + elementName + "']"));

                    elementFound = elements.Any(e =>
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
                }

                if (elementFound)
                {
                    Console.WriteLine(
                        elementName + " is displayed on " + pageName);
                    return;
                }

                Console.WriteLine(
                    elementName + " not visible. Swipe attempt: " + (i + 1));

                if (pageName == "SettingsPage" &&
                    elementName == "Sounddelay")
                {
                    int centerX = screenSize.Width / 2;
                    int startY = (int)(screenSize.Height * 0.75);
                    int endY = (int)(screenSize.Height * 0.25);

                    if (direction.Equals(
                        "Up",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        int temp = startY;
                        startY = endY;
                        endY = temp;
                    }

                    driver.ExecuteScript(
                        "mobile: dragGesture",
                        new Dictionary<string, object>
                        {
                            ["startX"] = centerX,
                            ["startY"] = startY,
                            ["endX"] = centerX,
                            ["endY"] = endY,
                            ["speed"] = 500
                        });
                }
                else
                {
                    object result = driver.ExecuteScript(
                        "mobile: scrollGesture",
                        new Dictionary<string, object>
                        {
                            ["left"] = 30,
                            ["top"] = (int)(screenSize.Height * 0.2),
                            ["width"] = screenSize.Width - 60,
                            ["height"] = (int)(screenSize.Height * 0.65),
                            ["direction"] = swipeDirection,
                            ["percent"] = 0.65
                        });

                    bool canScrollMore =
                        result != null && Convert.ToBoolean(result);   

                    Console.WriteLine(
                        "Can scroll more: " + canScrollMore);

                    Thread.Sleep(1000);

                    if (!canScrollMore)
                    {
                        Console.WriteLine(
                            "Scroll reached the end of the container.");
                        break;
                    }
                }

                Thread.Sleep(500);
            }

            throw new Exception(
                elementName +
                " was not displayed on " + pageName +
                " after scrolling.");
        }


        [Given("I press {string} on {string} if exists")]
        public void GivenIPressOnIfExists(
               string elementName,
               string pageName)
        {
            Console.WriteLine(
                "Checking whether " + elementName +
                " exists on " + pageName
            );

            try
            {
                bool isDisplayed =
                    GetPage(pageName).IsDisplayed(elementName);

                if (isDisplayed)
                {
                    GetPage(pageName).Tap(elementName);

                    Console.WriteLine(
                        elementName + " found and pressed."
                    );
                }
                else
                {
                    Console.WriteLine(
                        elementName + " does not exist. Skipping."
                    );
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    elementName + " does not exist. Skipping."
                );
            }
        }
        [Given("I swipe {string} on {string} and press {string} in {string}")]
        public void GivenISwipeOnAndPressIn(
               string left,
               string programBar,
               string elementToPress,
               string mainPage)
        {
            WhenISwipeOnAndPressIn(
                left,
                programBar,
                elementToPress,
                mainPage
            );
        }


        [When("I swipe {string} on {string} and press {string} in {string}")]
        public void WhenISwipeOnAndPressIn(
            string left,
            string programBar,
            string hearInNoise,
            string mainPage)
        {
            Console.WriteLine(
                "Swiping " + left + " on " + programBar +
                " and pressing " + hearInNoise +
                " on " + mainPage
            );

            BasePage page = GetPage(mainPage);

            if (!TryGetPageElementId(page, programBar, out string programBarId))
            {
                throw new Exception(
                    "Resource ID not found for " + programBar +
                    " on " + mainPage
                );
            }

            var programBarElement = driver.FindElement(By.Id(programBarId));

            if (!programBarElement.Displayed)
            {
                throw new Exception(programBar + " is not displayed.");
            }

            var location = programBarElement.Location;
            var size = programBarElement.Size;

            var screenSize = driver.Manage().Window.Size;

            int safeLeft = Math.Max(location.X + 150, 150);

            int safeRight = Math.Min(
                location.X + size.Width - 150,
                screenSize.Width - 150
            );

            int swipeWidth = safeRight - safeLeft;

            if (swipeWidth <= 100)
            {
                throw new Exception(
                    "ProgramBar is too narrow for a safe horizontal swipe. " +
                    "ProgramBar width: " + size.Width
                );
            }

            int centerY = location.Y + (size.Height / 2);

            int swipeTop = Math.Max(centerY - 30, 50);
            int swipeBottom = Math.Min(
                centerY + 30,
                screenSize.Height - 100
            );

            int swipeHeight = swipeBottom - swipeTop;

            if (swipeHeight <= 0)
            {
                throw new Exception(
                    "Invalid swipe area for ProgramBar."
                );
            }

            Console.WriteLine(
                "ProgramBar bounds: X=" + location.X +
                ", Y=" + location.Y +
                ", Width=" + size.Width +
                ", Height=" + size.Height
            );

            Console.WriteLine(
                "Safe swipe area: Left=" + safeLeft +
                ", Top=" + swipeTop +
                ", Width=" + swipeWidth +
                ", Height=" + swipeHeight
            );
            string direction = left.Trim().ToLower();

            if (direction != "left" && direction != "right")
            {
                throw new Exception(
                    "Invalid horizontal swipe direction: " + left
                );
            }

            driver.ExecuteScript(
                "mobile: swipeGesture",
                new Dictionary<string, object>
                {
                    ["left"] = safeLeft,
                    ["top"] = swipeTop,
                    ["width"] = swipeWidth,
                    ["height"] = swipeHeight,
                    ["direction"] = direction,
                    ["percent"] = 0.30
                }
            );

            Thread.Sleep(1000);

            Console.WriteLine(
                "Swiped " + direction + " on " + programBar
            );
            if (TryGetPageElementId(page, hearInNoise, out string programId))
            {
                page.TapByResourceId(programId, hearInNoise);
            }
            else
            {
                page.Tap(hearInNoise);
            }

            Console.WriteLine(
                hearInNoise + " clicked successfully."
            );
        }


        [When("I set {string} as {string} on {string}")]
        public void WhenISetAsOn(
            string elementName,
            string value,
            string pageName)
        {

            if (pageName == "MainPage" &&
                elementName == "Volume")
            {
                if (!int.TryParse(value, out int volumeValue))
                {
                    throw new Exception(
                        "Invalid Volume value: " + value);
                }

                MainPage mainPage = new MainPage(driver);

                mainPage.SetVolumeValue(volumeValue);

                Console.WriteLine(
                    "Volume value set to: " + volumeValue);
            }
            else if (pageName == "MainPage" &&
                       elementName.Equals("Leftvolume", StringComparison.OrdinalIgnoreCase))
            {
                int volumeValue = int.Parse(value);

                new MainPage(driver).SetLeftVolumeValue(volumeValue);
            }
            else if (pageName == "MainPage" &&
                     elementName.Equals("Rightvolume", StringComparison.OrdinalIgnoreCase))
            {
                int volumeValue = int.Parse(value);

                new MainPage(driver).SetRightVolumeValue(volumeValue);
            }
            else if (pageName == "EqualizerPresetPage" &&
                     elementName == "Bass")
            {
                if (!int.TryParse(value, out int bassValue))
                {
                    throw new Exception(
                        "Invalid Bass value: " + value);
                }

                EqualizerPresetPage equalizerPage =
                    new EqualizerPresetPage(driver);

                equalizerPage.SetBassValue(bassValue);

                Console.WriteLine(
                    "Bass value set to: " + bassValue);
            }
            else
            {
                throw new Exception(
                    "Unsupported element or page: " +
                    elementName + " on " + pageName);
            }
        }

        private bool IsElementDisplayedOnPage(
            string pageName,
            string elementName)
        {
            Console.WriteLine(
                "Checking visibility of " + elementName +
                " on " + pageName);

            BasePage page = GetPage(pageName);

            // First, use the page-specific verification method if available.
            var customMethod = page.GetType().GetMethod(
                "IsElementDisplayed",
                new[] { typeof(string) });

            if (customMethod != null)
            {
                try
                {
                    bool result = (bool)customMethod.Invoke(
                        page,
                        new object[] { elementName });

                    if (result)
                    {
                        Console.WriteLine(
                            elementName + " found using page-specific verification.");

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Page-specific verification failed: " + ex.Message);
                }
            }

            // Next, use the generic BasePage verification.
            try
            {
                if (page.IsDisplayed(elementName))
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "BasePage verification fallback: " + ex.Message);
            }

            // Final fallback: search by visible text or content description.
            string xpath =
                "//*[@text=\"" + elementName +
                "\" or @content-desc=\"" + elementName + "\"]";

            var elements = driver.FindElements(By.XPath(xpath));

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


        [Then("I scroll {string} to {string} on {string}")]
        public void ThenIScrollToOn(
            string direction,
            string elementName,
            string pageName)
        {
            Console.WriteLine(
                $"Scrolling {direction} to {elementName} on {pageName}");

            bool isDisplayed = false;

            for (int attempt = 1; attempt <= 8; attempt++)
            {
                isDisplayed = GetPage(pageName).IsDisplayed(elementName);

                if (isDisplayed)
                {
                    Console.WriteLine(
                        $"{elementName} is displayed on {pageName}.");
                    return;
                }

                Console.WriteLine(
                    $"Element not visible. Scroll attempt: {attempt}");


                driver.ExecuteScript(
                    "mobile: scrollGesture",
                    new Dictionary<string, object>
                    {
                        ["left"] = 100,
                        ["top"] = 300,
                        ["width"] = 880,
                        ["height"] = 1700,
                        ["direction"] = direction.ToLower(),
                        ["percent"] = 0.75
                    });

                Thread.Sleep(1000);
            }


            isDisplayed = GetPage(pageName).IsDisplayed(elementName);

            NUnit.Framework.Assert.That(
                isDisplayed,
                NUnit.Framework.Is.True,
                $"{elementName} was not displayed on {pageName}");
        }
    }
}