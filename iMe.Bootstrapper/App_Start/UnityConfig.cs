using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using iMe.Business;
using iMe.Integration;
using iMe.Integration.Helpers;
using iMe.Integration.Services;
using iMe.Interfaces;
using iMe.IServices;
using iMe.Mapper;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

using Unity.WebApi;
using iMe.Services;

namespace iMe.Bootstrapper
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(CreateContainer);

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        /// <summary>
        /// Create the DI container
        /// </summary>
        private static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            RegisterComponents(container);
            container.AddNewExtension<Interception>();
            
            return container;
        }

        public static void RegisterComponents(IUnityContainer container)
        {
            // Register all your components with the container here
            // It is NOT necessary to register your controllers
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
                new Tuple<string, Type, Type>("github", typeof(ISocialNetworkProvider), typeof(GitHubProvider)),
                new Tuple<string, Type, Type>("twitter", typeof(ISocialNetworkProvider), typeof(TwitterProvider))
            };

            //No borrar esto
            //container.RegisterType<ISocialNetworkProvider,TwitterProvider>("twitter");
            //container.RegisterType<ISocialNetworkProvider, GitHubProvider>("github");

            var socialServiceTypes = RegisterSocialNetworkServices(container, socialNetworkRegistrationTypes);
            RegisterBroadcastService(container, socialServiceTypes);

            container.RegisterType<IPersonalInfoService, PersonalInfoService>();
      
            container.RegisterType<ISocialNetworkServiceLocator, SocialNetworkServiceLocator>();
        }

        private static void RegisterBroadcastService(IUnityContainer container, List<object> serviceParams)
        {
            // Constructor injection must be configured manually
            // to avoid recursion on type resolution in the ServiceLocator
            container.RegisterType<ISocialNetworkProvider, BroadcastProvider>("broadcast",
                new InjectionConstructor(serviceParams.Cast<ISocialNetworkProvider>().ToArray(),
                    container.Resolve<IEntityMapper>()));
        }

        private static List<object> RegisterSocialNetworkServices(IUnityContainer container, Tuple<string, Type, Type>[] socialNetworkRegistrationTypes)
        {
            var serviceParams = new List<object>();

            foreach (var tuple in socialNetworkRegistrationTypes)
            {
                container.RegisterType(tuple.Item2, tuple.Item3, tuple.Item1, 
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<ExceptionBehavior>());
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