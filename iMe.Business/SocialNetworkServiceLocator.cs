using System;
using System.Collections.Generic;
using System.Linq;

using iMe.Common;
using iMe.Integration;

// ReSharper disable once PossibleMistakenCallToGetType.2
namespace iMe.Business
{
    public class SocialNetworkServiceLocator : ISocialNetworkServiceLocator
    {
        private readonly ISocialNetworkService[] serviceList;

        public SocialNetworkServiceLocator(ISocialNetworkService[] serviceList)
        {
            this.serviceList = serviceList;
        }

        public IList<ISocialNetworkService> GetAll()
        {
            return this.serviceList;
        }

        public ISocialNetworkService GetInstance(string clientTypeName)
        {
            this.ValidateClientType(clientTypeName);

            return this.serviceList.First(s => s.SocialNetworkName.ToString().ToLowerInvariant().Contains(clientTypeName));
        }

        public ISocialNetworkService GetInstance(Type clientType)
        {
            if (clientType == null) throw new ArgumentNullException(nameof(clientType));

            var clientTypeName = clientType.Name.ToLowerInvariant();
            this.ValidateClientType(clientTypeName.Replace("service", string.Empty));

            return this.serviceList.First(s => s.SocialNetworkName.ToString().ToLowerInvariant().Contains(clientTypeName));
        }

        private void ValidateClientType(string clientType)
        {
            SocialNetworks value;
            Enum.TryParse(clientType, true, out value);

            if (value == SocialNetworks.None)
            {
                throw new ArgumentException($"Invalid social network service type: {clientType}", nameof(clientType));
            }
        }
    }
}