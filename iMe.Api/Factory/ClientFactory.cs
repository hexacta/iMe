using iMe.Interfaces;
using Microsoft.Practices.Unity;


namespace iMe.Factory
{
    public static class ClientFactory
    {
        public static ISocialNetworkClient GetClient(string clientType)
        {
            var container = UnityConfig.GetConfiguredContainer();

            switch (clientType.ToLowerInvariant())
            {
                case "twitter":
                    return container.Resolve<ISocialNetworkClient>("twitter");
                default:
                    return container.Resolve<ISocialNetworkClient>();
            }
        }
    }
}