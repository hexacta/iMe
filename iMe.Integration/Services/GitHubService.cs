using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using iMe.Common;
using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Integration.Services
{
    public class GitHubService : ISocialNetworkService
    {
        private readonly IEntityMapper mapper;

        private readonly IHttpHelper httpHelper;

        public SocialNetworks SocialNetworkName => SocialNetworks.GitHub;

        public GitHubService(IEntityMapper mapper, IHttpHelper httpHelper)
        {
            this.mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<IList<SocialClientResponse>> GetPersonalInfo(string userId)
        {
            IList<SocialClientResponse> personalInfo = new List<SocialClientResponse>();
            IList<GitHubUserResponse> gitHubUserResponse = new List<GitHubUserResponse>();
            var client = this.httpHelper.GetConfiguredHttpClient(ConfigKeys.GitHubUserApiSearchUrl);

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + userId);
            if (response.IsSuccessStatusCode)
            {
                var gitUserResponse = await response.Content.ReadAsAsync<GitHubUserResponse>();
                if (gitUserResponse != null)
                {
                    gitHubUserResponse.Add(gitUserResponse);
                }
            }

            personalInfo = this.mapper.Map<IList<GitHubUserResponse>, IList<SocialClientResponse>>(gitHubUserResponse);
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