
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace ArwenAutomation.Reports
{
    public static class ExtentReportManager
    {
        private static ExtentReports? extent;

        public static string ReportDirectory { get; private set; } = "";

        public static string ScreenshotDirectory { get; private set; } = "";

        public static void InitializeReport()
        {
            string projectDirectory = FindProjectDirectory();

            ReportDirectory = Path.Combine(
                projectDirectory,
                "Reports");

            ScreenshotDirectory = Path.Combine(
                ReportDirectory,
                "Screenshots");

            Directory.CreateDirectory(ReportDirectory);
            Directory.CreateDirectory(ScreenshotDirectory);

            string reportPath = Path.Combine(
                ReportDirectory,
                "ExtentReport.html");

            ExtentSparkReporter sparkReporter =
                new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            extent.AddSystemInfo("Project", "Arwen Automation");
            extent.AddSystemInfo("Framework", "Appium + C#");
            extent.AddSystemInfo("BDD", "Reqnroll");

            Console.WriteLine("Report created: " + reportPath);
        }

        public static ExtentTest CreateTest(string scenarioName)
        {
            if (extent == null)
            {
                throw new InvalidOperationException(
                    "Extent report is not initialized.");
            }

            return extent.CreateTest(scenarioName);
        }

        public static void FlushReport()
        {
            extent?.Flush();
        }

        private static string FindProjectDirectory()
        {
            DirectoryInfo? directory =
                new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directory != null)
            {
                if (File.Exists(
                    Path.Combine(directory.FullName, "Arwen.csproj")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Could not find Arwen.csproj.");
        }
    }
}
