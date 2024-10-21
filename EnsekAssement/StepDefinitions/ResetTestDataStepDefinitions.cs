using AventStack.ExtentReports;
using EnsekAssement.Class;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EnsekAssement.StepDefinitions
{
    [Binding]
    public class ResetTestDataStepDefinitions
    {
        public ExtentTest test;
        public readonly ScenarioContext scenarioContext;
        private ResetTestDataClass resetTestDataClass;

        public ResetTestDataStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            resetTestDataClass = new ResetTestDataClass(scenarioContext);
        }

        [Given(@"reset the test data and verify '([^']*)' and '([^']*)'")]
        public async Task GivenResetTheTestDataAndVerifyAnd(int expectedStatusCode, string comments)
        {
            await resetTestDataClass.ResetTheTestData(expectedStatusCode, comments);
        }

        [Then(@"validate the response status")]
        public async Task ThenValidateTheResponseStatus()
        {
            if(ResetTestDataClass.pass)
            {
                Assert.Pass("Reset test data scenario " + scenarioContext.ScenarioInfo.Title + " passed");
            }
            else
            {
                Assert.Fail("Reset test data scenario " + scenarioContext.ScenarioInfo.Title + " failed");
            }
        }
    }
}
