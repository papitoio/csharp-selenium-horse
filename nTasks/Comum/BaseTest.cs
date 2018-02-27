using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;

namespace nTasks.Comum
{
    public class BaseTest
    {
        public ExtentReports extent;
        public ExtentTest test;

        public IWebDriver driver;

        private string browser;

        private string reportDir;

        [OneTimeSetUp]
        public void Reports()
        {
            this.reportDir = @"c:\\qaninja\\reports\\";
            var reportFile = this.GetType().ToString();

            var htmlReporter = new ExtentHtmlReporter(reportDir + reportFile + ".html");

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void Browser()
        {
            this.browser = ConfigurationManager.AppSettings["browser"];

            if (browser == "Chrome")
            {
                var option = new ChromeOptions();
                option.AddArgument("--headless");
                driver = new ChromeDriver(option);
            }

            if (browser == "Firefox")
            {
                driver = new FirefoxDriver();
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Finish()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            var testName = TestContext.CurrentContext.Test.Name;

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    test.Fail("screenshot").AddScreenCaptureFromPath(TakeShot(testName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    test.Pass("screenshot").AddScreenCaptureFromPath(TakeShot(testName));
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extent.Flush();


            driver.Close();
        }

        public string TakeShot(string testName)
        {
            var ts = (ITakesScreenshot)driver;

            var shot = ts.GetScreenshot();

            var shotPath = this.reportDir + @"shots\\" + testName + ".png";
            var local = new Uri(shotPath).LocalPath;
            shot.SaveAsFile(local, ScreenshotImageFormat.Png);
            return local;
        }
    }
}
