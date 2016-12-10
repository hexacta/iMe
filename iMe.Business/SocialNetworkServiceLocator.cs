using System;
using System.Collections.Generic;
using System.Linq;
using iMe.Business.Exceptions;
using iMe.Common;
using iMe.Integration;

namespace iMe.Business
{
    public class SocialNetworkServiceLocator : ISocialNetworkServiceLocator
    {
        private readonly ISocialNetworkProvider[] _providerList;

        public SocialNetworkServiceLocator(ISocialNetworkProvider[] _providerList)
        {
            this._providerList = _providerList;
        }

        public IList<ISocialNetworkProvider> GetAll()
        {
            return this._providerList;
        }

        public ISocialNetworkProvider GetInstance(string clientTypeName)
        {
            this.ValidateClientType(clientTypeName);

            return this._providerList.First(s => s.SocialNetworkName.ToString().ToLowerInvariant().Contains(clientTypeName));
        }

        public ISocialNetworkProvider GetInstance(Type clientType)
        {
            if (clientType == null) throw new ArgumentNullException(nameof(clientType));

            var clientTypeName = clientType.Name.ToLowerInvariant();
            this.ValidateClientType(clientTypeName.Replace("provider", string.Empty));

            return this._providerList.First(s => s.SocialNetworkName.ToString().ToLowerInvariant().Contains(clientTypeName));
        }

        private void ValidateClientType(string clientType)
        {
            SocialNetworks value;
            Enum.TryParse(clientType, true, out value);

            if (value == SocialNetworks.None)
            {
                throw new SocialNetworkNotFoundException($"Invalid social network service type: {clientType}", nameof(clientType));
            }
        }
    }
}