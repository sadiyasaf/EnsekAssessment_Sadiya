using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace EnsekAssement.Utils
{
    [Binding]
    public sealed class Hooks
    {
        public static ExtentTest test;
        public static ExtentSparkReporter htmlReporter;
        public static ExtentReports extent;
        public static ExtentTest parent;
        public static ExtentTest child;
        public static string date = DateTime.Now.ToString("ddMMyyyy_HHmmss");
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        public static string BaseUrl;
        public static string UserName;
        public static string Password;
        public static string comments = "";

        // Constructor to initialize ScenarioContext and FeatureContext
        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BasicSetUp()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string reportPath = Path.Combine(projectDirectory, "Reports", $"API test report_{date}", "index.html");
            htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AddSystemInfo("OS", "Windows");
            extent.AttachReporter(htmlReporter);

            // Ensure the directory exists
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
            }
        }

        [BeforeFeature]
        public static void BeforeFeatureFile(FeatureContext featureContext)
        {
            parent = extent.CreateTest(featureContext.FeatureInfo.Title);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = configuration.Get<Settings>();
            BaseUrl = settings.AppSettings.BaseUrl;
            UserName = settings.AppSettings.username;
            Password = settings.AppSettings.password;
        }

        [BeforeScenario]
        public void ReportSetup()
        {
            child = parent.CreateNode(scenarioContext.ScenarioInfo.Title);
            scenarioContext.Add("test", child);
            test = (ExtentTest)scenarioContext["test"];
        }

        [AfterScenario]
        public void CleanupScenario()
        {
            // Dispose of scenario context if it is disposable
            if (scenarioContext is IDisposable disposableContext)
            {
                disposableContext.Dispose();
            }
        }

        [AfterStep]
        public void AfterStepExecution()
        {
            comments = "";
        }

        [AfterTestRun]
        public static void EndReport()
        {
            extent.Flush();
        }
    }
}

