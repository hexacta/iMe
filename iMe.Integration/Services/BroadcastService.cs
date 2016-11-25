using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using iMe.Common;
using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Integration.Services
{
    public class BradcastService : ISocialNetworkService
    {
        private readonly IEntityMapper mapper;

        private readonly ISocialNetworkService[] socialNetworkServices;

        public SocialNetworks SocialNetworkName => SocialNetworks.Broadcast;

        public BradcastService(/*ISocialNetworkService[] socialNetworkServices, */IEntityMapper mapper)
        {
            this.mapper = mapper;
            this.socialNetworkServices = socialNetworkServices?.Where(s => s.SocialNetworkName != this.SocialNetworkName).ToArray();
        }

        public async Task<IList<SocialClientResponse>> GetPersonalInfo(string userId)
        {
            List<SocialClientResponse> serviceResponse = new List<SocialClientResponse>();

            foreach (var socialNetworkService in this.socialNetworkServices)
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