using EnsekAssement.Class;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EnsekAssement.StepDefinitions
{
    [Binding]
    public class BuyFuelStepDefinitins
    {
        public readonly ScenarioContext scenarioContext;
        private HttpClient httpClient;
        private BuyFuelClass buyFuelClass;

        public BuyFuelStepDefinitins(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient();
            buyFuelClass = new BuyFuelClass(scenarioContext);

        }

        [Given(@"Retrieve energyIds '([^']*)' and quantity '([^']*)' for '([^']*)' and buy each fuel and verify '([^']*)' with '([^']*)'")]
        public async Task GivenRetrieveEnergyIdsAndQuantityForAndBuyEachFuelAndVerifyWith(int energyId, int quantity, string fuel, int expectedStatusCode, string comments)
        {
            await buyFuelClass.BuyFuel(energyId, quantity, fuel, expectedStatusCode, comments);
        }

        [Then(@"verify the orders list contains the purchased and verify the status code '([^']*)' with '([^']*)'")]
        public async Task ThenVerifyTheOrdersListContainsThePurchasedAndVerifyTheStatusCodeWith(int expectedStatusCode, string comments)
        {
            await buyFuelClass.VerifytheOrderId(expectedStatusCode, comments);
            if (BuyFuelClass.orderIdFound)
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
        
        
    

               
               
       
