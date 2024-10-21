using EnsekAssement.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekAssement.Requests
{
    class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginRequest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            var settings = configuration.Get<Settings>();
            this.username = settings.AppSettings.username.ToString();
            this.password = settings.AppSettings.password.ToString();
        }
        public LoginRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
