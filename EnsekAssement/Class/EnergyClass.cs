using AventStack.ExtentReports;
using EnsekAssement.Responses;
using EnsekAssement.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using EnsekAssement.Networking;

namespace EnsekAssement.Class
{
    public class EnergyClass
    {
        private HttpClient httpClient;
        public readonly ScenarioContext scenarioContext;
        public static bool pass = false;
        public ExtentTest test;
        private EnergyClass energyClass;
        public EnergyClass(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(Hooks.BaseUrl)
            };
        }
        public async Task<bool> GettheEnergyDetails(int expectedStatusCode, string comments)
        {
            var request = "energy";
            test = (ExtentTest)scenarioContext["test"];
            try
            {
                httpClient= NetworkClient.AddAuthorisation(httpClient);
                var response = await httpClient.GetAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                var energyResponse = JsonConvert.DeserializeObject<EnergyResponse>(responseContent);
                int statusCodeFromResponse = (int)response.StatusCode;

                JObject data = JObject.Parse(responseContent);
                var energyIds = new Dictionary<string, EnergyResponse>();
                foreach (var energyType in data)
                {
                    string keyPrefix = energyType.Key;
                    var energyDetails = energyType.Value.ToString();

                    energyIds.Add(keyPrefix, JsonConvert.DeserializeObject<EnergyResponse>(energyDetails));
                    //Console.WriteLine("Energy Ids...." + energyIds);
                }

                //Storing enerygy Ids in scenario context
                scenarioContext.Remove("EnergyIds");
                scenarioContext.Add("EnergyIds", energyIds);

                if (statusCodeFromResponse == expectedStatusCode && statusCodeFromResponse == 200)
                {
                    pass = true;
                    test.Pass("Successfully retrieved energy details with status code " + statusCodeFromResponse);
                }
                else if (statusCodeFromResponse == expectedStatusCode && statusCodeFromResponse == 401)
                {
                    pass = true;
                    test.Pass("Unauthorized access correctly identified and endpoint returned " + statusCodeFromResponse);
                }
                else
                {
                    pass = false;
                    test.Fail();
                }
                return pass;
            }
            catch (Exception e)
            {
                test.Fail(scenarioContext.ScenarioInfo.Title + " failed due to " + e);
                return false;
            }
        }
    }
}
