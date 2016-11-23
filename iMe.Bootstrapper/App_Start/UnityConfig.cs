using System;
using System.Web.Http;

using iMe.Business;
using iMe.Integration;
using iMe.Integration.Helpers;
using iMe.Integration.Services;
using iMe.Interfaces;
using iMe.Mapper;

using Microsoft.Practices.Unity;

using Unity.WebApi;

namespace iMe.Bootstrapper
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(
                                                                      () =>
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
            container.RegisterType<IUnityContainer, UnityContainer>();

            RegisterServices(container);
            RegisterMappers(container);
            container.RegisterType<IHttpHelper, HttpClientHelper>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ISocialNetworkService, TwitterService>("twitter");
            container.RegisterType<ISocialNetworkService, GitHubService>("github");
            container.RegisterType<ISocialService, PersonalInfoService>();
        }

        private static void RegisterMappers(IUnityContainer container)
        {
            container.RegisterType<IEntityMapper, EntityMapper>();
        }
    }
}