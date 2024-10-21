using EnsekAssement.Responses;
using EnsekAssement.StepDefinitions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Events;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using TechTalk.SpecFlow;
using EnsekAssement.Utils;
using EnsekAssement.Class;

namespace EnsekAssement.Networking
{
    public class NetworkClient
    {
        public static string Bearertoken;
        public static HttpClient AddAuthorisation(HttpClient httpClient)
        {
            try
            {
                Bearertoken = Hooks.comments.Contains("Request unauthorized")? GenerateInvalidToken(30) : LoginClass.Token;
                httpClient.DefaultRequestHeaders
                    .Add("accept", "*/*");
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Bearertoken);
                return httpClient;
            }
            catch (Exception ex)
            {
                Assert.Fail("HttpClient failed due to" + ex);
                return httpClient;
            }
        }
        public static string GenerateInvalidToken(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
