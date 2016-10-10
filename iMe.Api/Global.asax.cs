using System.Net.NetworkInformation;
using System.Web.Http;

namespace iMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.GetConfiguredContainer();
        }
    }
}
