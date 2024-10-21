using AventStack.ExtentReports;
using EnsekAssement.Networking;
using EnsekAssement.Responses;
using EnsekAssement.Utils;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace EnsekAssement.Class
{
    public class OrderClass
    {
        private HttpClient httpClient;
        public readonly ScenarioContext scenarioContext;
        public ExtentTest test;

        public OrderClass(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(Hooks.BaseUrl)
            };
        }
        public async Task<int> VerifyOrdersBeforeCurrentDate()
        {
            test = (ExtentTest)scenarioContext["test"];
            var request = ApiUrls.Orders;
            int countBeforeCurrentDate = 0;
            httpClient = NetworkClient.AddAuthorisation(httpClient);
            var response = await httpClient.GetAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            int actualStatusCode = (int)response.StatusCode;
            var orderResponse = JsonConvert.DeserializeObject<List<OrdersResponse>>(responseContent);
            foreach (var order in orderResponse)
            {
                // Attempt to parse the date with the specified format
                if (DateTime.TryParseExact(order.time, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal,
                    out DateTime parsedDate))
                {
                    DateTime currentDate = DateTime.UtcNow.Date;

                    if (parsedDate < currentDate)
                    {
                        countBeforeCurrentDate++;
                    }
                }
                else
                {
                    //Skip invalid date format from the response of orders list
                    test.Skip("Invalid date format " + order.time);
                }
            }
            return countBeforeCurrentDate;
        }
    }
}
