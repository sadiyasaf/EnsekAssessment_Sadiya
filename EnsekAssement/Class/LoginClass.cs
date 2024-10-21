using AventStack.ExtentReports;
using EnsekAssement.Responses;
using EnsekAssement.Utils;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace EnsekAssement.Class
{
    public class LoginClass
    {
        public readonly ScenarioContext scenarioContext;
        public ExtentTest test;

        private readonly HttpClient httpClient;
        public static string Token;
        public static string Message;
        public static bool pass = false;
        public LoginClass(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            httpClient = new HttpClient();
        }
        public async Task<bool> LogInToSite(StringContent content, int expectedStatusCode)
        {
            test = (ExtentTest)scenarioContext["test"];
            try
            {
                httpClient.DefaultRequestHeaders
                    .Add("accept", "*/*");
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.BaseAddress = new Uri(Hooks.BaseUrl);

                var result = await httpClient.PostAsync("login", content);

                // Read and log the response content
                var resultContent = await result.Content.ReadAsStringAsync();
                var statusCodeFromResponse = (int)result.StatusCode;

                if (expectedStatusCode == statusCodeFromResponse && statusCodeFromResponse == 200)
                {
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(resultContent);
                    Token = loginResponse.access_token;
                    Message = loginResponse.message;

                    if (!Token.Equals(null) && Message.Equals("Success"))
                    {
                        pass = true;
                        test.Pass("Login successfull. Bearer Token " + Token + " Returned with status code " + statusCodeFromResponse);
                    }
                }
                else if (expectedStatusCode == statusCodeFromResponse && statusCodeFromResponse == 401)
                {
                    pass = true;
                    test.Pass("Unauthorized access test passed as expected with message" + Message + " and status code " + statusCodeFromResponse);
                }
                else
                {
                    pass = false;
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
