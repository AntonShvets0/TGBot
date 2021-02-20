using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TelegramBot.Api
{
    public abstract class Api
    {
        public string AccessToken { protected get; set; }

        private string HTTPRequest(string url, NameValueCollection args)
        {
            var web = new WebClient();
            try
            {
                return Encoding.UTF8.GetString(web.UploadValues(url, "POST", args));
            }
            catch (WebException ex)
            {
                var response = ex.Response.GetResponseStream();

                return response == null ? null : new StreamReader(response).ReadToEnd();
            }
        }
        
        protected JObject CallMethod(string method, NameValueCollection args) =>
            JObject.Parse(HTTPRequest($"https://api.telegram.org/bot{AccessToken}/{method}", args));
    }
}