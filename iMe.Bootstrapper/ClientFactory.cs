using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMe.Interfaces;
using Microsoft.Practices.Unity;

namespace iMe.Bootstrapper
{
    public class ClientFactory : IClientFactory
    {
        private IUnityContainer container;

        public ClientFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public ISocialNetworkClient GetClient(string clientType)
        {
            //var container = UnityConfig.GetConfiguredContainer();
            ISocialNetworkClient client = null;
            if (container != null && container.IsRegistered<ISocialNetworkClient>(clientType))
            {
                client = container.Resolve<ISocialNetworkClient>(clientType);
            }

            return client;


        }
    }
}
