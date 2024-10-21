using System;
using System.Collections.Generic;

namespace EnsekAssement.Utils
{
    public class Settings
    {
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}