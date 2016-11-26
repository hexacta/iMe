using System;
using System.Collections.Generic;
using System.Linq;
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

            RegisterMappers(container);
            RegisterHelpers(container);
            RegisterServices(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterHelpers(IUnityContainer container)
        {
            container.RegisterType<IHttpHelper, HttpClientHelper>();
        }

        private static void RegisterServices(IUnityContainer container)
        {
            Tuple<string, Type, Type>[] socialNetworkRegistrationTypes = {
                new Tuple<string, Type, Type>("github", typeof(ISocialNetworkService), typeof(GitHubService)),
                new Tuple<string, Type, Type>("twitter", typeof(ISocialNetworkService), typeof(TwitterService))
            };

            var socialServiceTypes = RegisterSocialNetworkServices(container, socialNetworkRegistrationTypes);
            RegisterBroadcastService(container, socialServiceTypes);

            container.RegisterType<ISocialService, PersonalInfoService>();
      
            container.RegisterType<ISocialNetworkServiceLocator, SocialNetworkServiceLocator>();
        }

        private static void RegisterBroadcastService(IUnityContainer container, List<object> serviceParams)
        {
            // Constructor injection must be configured manually
            // to avoid recursion on type resolution in the ServiceLocator
            container.RegisterType<ISocialNetworkService, BradcastService>("broadcast",
                new InjectionConstructor(serviceParams.Cast<ISocialNetworkService>().ToArray(),
                    container.Resolve<IEntityMapper>()));
        }

        private static List<object> RegisterSocialNetworkServices(IUnityContainer container, Tuple<string, Type, Type>[] socialNetworkRegistrationTypes)
        {
            var serviceParams = new List<object>();

            foreach (var tuple in socialNetworkRegistrationTypes)
            {
                container.RegisterType(tuple.Item2, tuple.Item3, tuple.Item1);
                serviceParams.Add(container.Resolve(tuple.Item2, tuple.Item1));
            }

            return serviceParams;
        }

        private static void RegisterMappers(IUnityContainer container)
        {
            container.RegisterType<IEntityMapper, EntityMapper>();
        }
    }
}