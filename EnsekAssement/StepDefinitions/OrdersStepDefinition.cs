using AventStack.ExtentReports;
using EnsekAssement.Class;
using TechTalk.SpecFlow;

namespace EnsekAssement.StepDefinitions
{
    [Binding]
    public class OrdersStepDefinition
    {
        private HttpClient httpClient;
        public readonly ScenarioContext scenarioContext;
        public ExtentTest test;
        private OrderClass orderClass;

        public OrdersStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient();
            orderClass = new OrderClass(scenarioContext);
        }

        [Given(@"Retrieve the orders from orders endpoint and return the number of orders created before current date")]
        public async Task GivenRetrieveTheOrdersFromOrdersEndpointAndReturnTheNumberOfOrdersCreatedBeforeCurrentDate()
        {
            await orderClass.VerifyOrdersBeforeCurrentDate();
        }
    }
}

