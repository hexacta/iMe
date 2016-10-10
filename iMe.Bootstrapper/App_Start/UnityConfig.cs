using Microsoft.Practices.Unity;
using NetworkAccess;
using System.Web.Http;
using TwitterAccess;
using Unity.WebApi;

namespace iMe.Bootstrapper
{
    public static class UnityConfig
    {
        

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ISocialNetworkClient, TwitterClient>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
