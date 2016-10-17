using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using NetworkAccess;
using TwitterAccess;
using Unity.WebApi;

namespace iMe
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = 
            new Lazy<IUnityContainer>(() =>
                {
                    var container = new UnityContainer();
                    RegisterComponents(container);
                    return container;
                });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterComponents(IUnityContainer container)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<ISocialNetworkClient, TwitterClient>("twitter");

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}