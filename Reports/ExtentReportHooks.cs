
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using Reqnroll;
using System;

namespace ArwenAutomation.Reports
{
    [Binding]
    public class ExtentReportHooks
    {
        private readonly ScenarioContext scenarioContext;

        private const string TestKey = "extentTest";

        public ExtentReportHooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportManager.InitializeReport();
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            ExtentTest test =
                ExtentReportManager.CreateTest(
                    scenarioContext.ScenarioInfo.Title);

            scenarioContext[TestKey] = test;
        }

        [AfterStep]
        public void AfterStep()
        {
            ExtentTest test =
                (ExtentTest)scenarioContext[TestKey];

            string stepName =
                scenarioContext.StepContext.StepInfo.Text;

            if (scenarioContext.TestError != null)
            {
                test.Log(Status.Fail, "Step Failed: " + stepName);

                test.Log(
                    Status.Fail,
                    scenarioContext.TestError.ToString());
            }
            else
            {
                test.Log(Status.Pass, "Step Passed: " + stepName);
            }
        }

        // Run before the driver cleanup in TestHooks.
        [AfterScenario(Order = 20000)]
        public void AfterScenario()
        {
            ExtentTest test =
                (ExtentTest)scenarioContext[TestKey];

            if (scenarioContext.TestError != null)
            {
                test.Fail(
                    "Scenario Failed: " +
                    scenarioContext.TestError.Message);

                CaptureScreenshot(test);
            }
            else
            {
                test.Pass("Scenario Passed");
            }
        }

        private void CaptureScreenshot(ExtentTest test)
        {
            if (!scenarioContext.ContainsKey("driver"))
            {
                test.Log(
                    Status.Warning,
                    "Driver not available for screenshot.");

                return;
            }

            try
            {
                AndroidDriver driver =
                    (AndroidDriver)scenarioContext["driver"];

                string screenshotName =
                    "FailedTest_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") +
                    ".png";

                string screenshotPath =
                    System.IO.Path.Combine(
                        ExtentReportManager.ScreenshotDirectory,
                        screenshotName);

                ITakesScreenshot screenshotDriver = driver;

                Screenshot screenshot =
                    screenshotDriver.GetScreenshot();

                screenshot.SaveAsFile(screenshotPath);

                test.AddScreenCaptureFromPath(
                    screenshotPath,
                    "Failure Screenshot");
            }
            catch (Exception ex)
            {
                test.Log(
                    Status.Warning,
                    "Screenshot failed: " + ex.Message);
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();

            Console.WriteLine(
                "Extent report generated successfully.");
        }
    }
}
