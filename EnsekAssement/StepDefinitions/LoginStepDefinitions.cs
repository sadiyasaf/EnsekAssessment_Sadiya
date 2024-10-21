using Newtonsoft.Json;
using TechTalk.SpecFlow;
using EnsekAssement.Requests;
using NUnit.Framework;
using AventStack.ExtentReports;
using EnsekAssement.Class;
using System.Text;

namespace EnsekAssement.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        public readonly ScenarioContext scenarioContext;
        public ExtentTest test;
        private readonly HttpClient httpClient;
        public static string Token;
        public static string Message;
        private LoginClass loginClass;
        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient();
            loginClass = new LoginClass(scenarioContext);
        }

        [Given(@"user login with '([^']*)' '([^']*)' '([^']*)'")]
        public async Task GivenuserLoginWith(string userName, string password, int expectedStatusCode)
        {
            if (userName == "" && password == "")
            {
                await loginClass.LogInToSite(new StringContent(JsonConvert.SerializeObject(new LoginRequest(), Formatting.Indented), Encoding.UTF8, "application/json"), expectedStatusCode);
            }
            else
            {
                await loginClass.LogInToSite(new StringContent(JsonConvert.SerializeObject(new LoginRequest(userName, password), Formatting.Indented), Encoding.UTF8, "application/json"), expectedStatusCode);
            }
        }

        [Then(@"verify if login is successfull")]
        public async Task ThenVerifyIfLoginIsSuccessfull()
        {
            if (LoginClass.pass)
            {
                Assert.Pass("Login test scenario " + scenarioContext.ScenarioInfo.Title+ " passed ");
            }
            else
            {
                Assert.Fail("Login test scenario " + scenarioContext.ScenarioInfo.Title + " failed");
            }
        }
    }
}