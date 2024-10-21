using AventStack.ExtentReports;
using EnsekAssement.Networking;
using EnsekAssement.Responses;
using EnsekAssement.Utils;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace EnsekAssement.Class
{
    public class ResetTestDataClass
    {
        private HttpClient httpClient;
        public static string Token;
        public ExtentTest test;
        public readonly ScenarioContext scenarioContext;
        public readonly FeatureContext featureContext;
        public static bool pass = false;

        public ResetTestDataClass(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(Hooks.BaseUrl)
            };
        }
        public async Task ResetTheTestData(int expectedStatusCode, string comments)
        {
            test = (ExtentTest)scenarioContext["test"];
            Hooks.comments = comments;
            var request = ApiUrls.Reset;
            try
            {
                httpClient = NetworkClient.AddAuthorisation(httpClient);
                var response = await httpClient.PostAsync(request, null);
                var responseContent = await response.Content.ReadAsStringAsync();
                var resetTestDataResponse = JsonConvert.DeserializeObject<ResetTestDataResponse>(responseContent);
                
                int statusCodeFromResponse = (int)response.StatusCode;
                string messageFromResponse = resetTestDataResponse.message;

                if (statusCodeFromResponse == 200 && statusCodeFromResponse.Equals(expectedStatusCode))
                {
                    if (comments.Contains("Success") && messageFromResponse.Contains("Success"))
                    {
                        pass = true;
                        test.Pass("Test data reset successfully completed with status code " + statusCodeFromResponse + "and success message" + messageFromResponse);
                    }
                }
                else if (statusCodeFromResponse == 401 && statusCodeFromResponse.Equals(expectedStatusCode))
                {
                    if (comments.Contains("Request unauthorized") && messageFromResponse.Equals("Unauthorized"))
                    {
                        pass = true;
                        test.Pass("Unauthorized access test passed as expected with status code " + statusCodeFromResponse);
                    }
                }
                else
                {
                    pass = false;
                    test.Fail();
                }
            }
            catch (Exception e)
            {
                test.Fail("Test data reset failed due to " + e);
            }
        }
    }
}
