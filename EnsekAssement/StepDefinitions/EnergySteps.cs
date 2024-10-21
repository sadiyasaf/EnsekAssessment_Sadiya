using AventStack.ExtentReports;
using EnsekAssement.Class;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EnsekAssement.StepDefinitions
{
    [Binding]
    public class EnergySteps
    {
        private HttpClient httpClient;
        public readonly ScenarioContext scenarioContext;
        public ExtentTest test;
        private EnergyClass energyClass;

        public EnergySteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient();
            energyClass = new EnergyClass(scenarioContext);
        }

        [Given(@"get the list of fuels and their quantities and verify the '([^']*)' with '([^']*)'")]
        public async Task GivenGetTheListOfFuelsAndTheirQuantitiesAndVerifyTheWith(int expectedStatusCode, string comments)
        {
            await energyClass.GettheEnergyDetails(expectedStatusCode, comments);
        }

        [Given(@"get the list of fuels and their quantities with '([^']*)' and '([^']*)'")]
        public async Task GivenGetTheListOfFuelsAndTheirQuantitiesWithAnd(int expectedStatusCode, string comments)
        {
            await energyClass.GettheEnergyDetails(expectedStatusCode, comments);
        }

        [Then(@"verify the response status")]
        public async Task ThenVerifyTheResponseStatus()
        {
            if (EnergyClass.pass)
            {
                Assert.Pass("Energy test scenario " + scenarioContext.ScenarioInfo.Title + " passed ");
            }
            else
            {
                Assert.Fail("Energy test scenario " + scenarioContext.ScenarioInfo.Title + " failed");
            }
        }
    }
}
