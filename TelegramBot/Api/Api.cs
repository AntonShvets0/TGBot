﻿using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TelegramBot.Api
{
    public abstract class Api
    {
        public string AccessToken { protected get; set; }

        private string HTTPRequest(string url, NameValueCollection args) =>
            Encoding.UTF8.GetString(new WebClient().UploadValues(url, "POST", args));

        protected JObject CallMethod(string method, NameValueCollection args) =>
            JObject.Parse(HTTPRequest($"https://api.telegram.org/bot{AccessToken}/{method}", args));
    }
}