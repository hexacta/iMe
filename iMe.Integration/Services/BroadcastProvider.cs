using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using iMe.Common;
using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Integration.Services
{
    public class BroadcastProvider : ISocialNetworkProvider
    {
        private readonly IEntityMapper mapper;

        private readonly ISocialNetworkProvider[] _socialNetworkProviders;

        public SocialNetworks SocialNetworkName => SocialNetworks.Broadcast;

        public BroadcastProvider(ISocialNetworkProvider[] _socialNetworkProviders, IEntityMapper mapper)
        {
            this.mapper = mapper;
            this._socialNetworkProviders = _socialNetworkProviders?.Where(s => s.SocialNetworkName != this.SocialNetworkName).ToArray();
        }

        public async Task<IList<SocialClientResponse>> GetPersonalInfo(string userId)
        {
            List<SocialClientResponse> serviceResponse = new List<SocialClientResponse>();

            foreach (var socialNetworkService in this._socialNetworkProviders)
            {
                serviceResponse.AddRange(await socialNetworkService.GetPersonalInfo(userId));
            }

            var personalInfo = this.mapper.Map<IList<SocialClientResponse>, IList<SocialClientResponse>>(
                serviceResponse);

            return personalInfo;
        }

        public Task<IList<SocialClientResponse>> GetPersonalInfo(string clientType, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }
    }
}