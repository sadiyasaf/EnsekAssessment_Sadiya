using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekAssement.Responses
{
    class LoginResponse
    {
        [JsonProperty("access_token")]
        public string access_token {  get; set; }
        
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
