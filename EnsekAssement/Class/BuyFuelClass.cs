using AventStack.ExtentReports;
using EnsekAssement.Networking;
using EnsekAssement.Responses;
using EnsekAssement.StepDefinitions;
using EnsekAssement.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace EnsekAssement.Class
{
    public class BuyFuelClass
    {
        public readonly ScenarioContext scenarioContext;
        private HttpClient httpClient;
        public static bool orderIdFound = false;
        public ExtentTest test;

        public BuyFuelClass(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(Hooks.BaseUrl)
            };
        }
        public async Task BuyFuel(int energyId, int quantity, string fuel, int expectedStatusCode, string comments)
        {
            bool pass;
            // Declare response and responseContent as local variables
            HttpResponseMessage response = null;
            string responseContent = null;
            int quantityAvailable = energyId == 1 ? 0 : quantity;
            int actualStatusCode = 0;
            List<int> parameters = new List<int>();
            httpClient = NetworkClient.AddAuthorisation(httpClient);
            var request = ApiUrls.Buy;

            if (energyId == 1)
            {
                EnergyClass energyClass = new EnergyClass(scenarioContext);

                await energyClass.GettheEnergyDetails(expectedStatusCode, comments);
                Dictionary<string, EnergyResponse> energyIds = scenarioContext.Get<Dictionary<string, EnergyResponse>>("EnergyIds");
                foreach (var item in energyIds)
                {
                    if (item.Key.Equals(fuel))
                    {
                        if (energyId == 1)
                        {
                            energyId = item.Value.energy_id;
                        }
                        quantityAvailable = item.Value.quantity_of_units;
                    }
                }
            }
            if (quantityAvailable > 0)
            {
                response = await httpClient.PutAsync(request + "/" + energyId + "/" + quantity, null);
                responseContent = await response.Content.ReadAsStringAsync();
                actualStatusCode = (int)response.StatusCode;
            }
            else
            {
                Assert.Pass("There is no stock left to purchase");
            }

            //verify the status code
            if (actualStatusCode.Equals(expectedStatusCode) && responseContent.Contains("order"))
            {
                int startind = responseContent.LastIndexOf(" ") + 1;
                int lastind = responseContent.LastIndexOf(".");

                int lenthOfId = lastind - startind;
                string orderId = responseContent.Substring(startind, lenthOfId);

                scenarioContext.Remove("OrderId");
                scenarioContext.Add("OrderId", orderId);
                pass = true;
            }
            else if (actualStatusCode.Equals(expectedStatusCode) && actualStatusCode.Equals(400))
            {
                Assert.Pass("Bad request identified : Purchase failed with status code " + expectedStatusCode);
            }
            else if (actualStatusCode.Equals(expectedStatusCode) && actualStatusCode.Equals(401))
            {
                Assert.Pass("Bad request identified : Purchase failed with status code " + expectedStatusCode);
            }
        }

        public async Task VerifytheOrderId(int expectedStatusCode, string comments)
        {
            test = (ExtentTest)scenarioContext["test"];
            var request = ApiUrls.Orders;
            httpClient = NetworkClient.AddAuthorisation(httpClient);
            var response = await httpClient.GetAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            int actualStatusCode = (int)response.StatusCode;
            var orderResponse = JsonConvert.DeserializeObject<List<OrdersResponse>>(responseContent);
            string orderIdWhenPurchased = scenarioContext.Get<string>("OrderId"); //get the order id assigned during purchase

            foreach (var order in orderResponse)
            {
                if (order.id.Equals(orderIdWhenPurchased))
                {
                    orderIdFound = true;
                    test.Pass("Order id list contains the order purchased");
                }
            }
            if (!orderIdFound)
            {
                orderIdFound = false;
                test.Fail("Order ID " + orderIdWhenPurchased + " not found in the order list");
            }
        }
    }
}
