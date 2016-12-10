using System.Net.Http;

namespace iMe.Integration
{
    public interface IHttpHelper
    {
        HttpClient GetConfiguredHttpClient(string baseRequestUrl);
    }
}