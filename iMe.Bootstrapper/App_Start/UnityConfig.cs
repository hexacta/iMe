using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using iMe.Bootstrapper.App_Start;
using iMe.Integration;
using iMe.Integration.Clients;
using iMe.Interfaces;
using iMe.Mapper;
using Unity.WebApi;
using iMe.Business;
using iMe.Integration.Helpers;

namespace iMe.Bootstrapper
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
           
            container.RegisterType<ISocialNetworkService, TwitterService>("twitter");
            container.RegisterType<ISocialNetworkService, GitHubService>("github");
            container.RegisterType<ISocialNetworkService, GenericService>();
            container.RegisterType<IUnityContainer, UnityContainer>();

            container.RegisterType<IEntityMapper, EntityMapper>();

            container.RegisterType<IHttpHelper, HttpClientHelper>();
            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
