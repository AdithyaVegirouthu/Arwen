
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace ArwenAutomation.Support
{
    public class DriverFactory
    {
        public static AndroidDriver CreateDriver()
        {
            AppiumOptions options = new AppiumOptions();

            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";
            options.DeviceName = "Android";
            
            options.AddAdditionalAppiumOption( "appPackage","com.gnhearing.arwen.development");
            options.AddAdditionalAppiumOption("appActivity","crc64c2d0320a8699986c.MainActivity");
            options.AddAdditionalAppiumOption("noReset", true);
           
            options.AddAdditionalAppiumOption("appWaitDuration", 30000);
            options.AddAdditionalAppiumOption(
              "settings[waitForIdleTimeout]", 1000);

            AndroidDriver driver = new AndroidDriver(new Uri("http://127.0.0.1:4723"),options,TimeSpan.FromSeconds(60));

            driver.Manage().Timeouts().ImplicitWait =
               
                TimeSpan.FromSeconds(2);

            return driver;
        }
    }
}
