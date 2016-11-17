using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iMe.Integration
{
    public interface IHttpHelper
    {
        HttpClient GetConfiguredHttpClient(string baseRequestUrl);
    }
}
