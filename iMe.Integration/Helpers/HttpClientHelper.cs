using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iMe.Integration.Helpers
{
    public class HttpClientHelper : IHttpHelper
    {
        public HttpClient GetConfiguredHttpClient(string baseRequestUrl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseRequestUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
            //github pide UserAgent
            return client;
        }
    }
}
