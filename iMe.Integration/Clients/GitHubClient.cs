using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using iMe.Dto;
using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Integration.Clients
{
    public class GitHubClient : ISocialNetworkClient
    {
        private readonly IEntityMapper mapper;
        private readonly IHttpHelper httpHelper;

        public SocialNetworks SocialNetworkName => SocialNetworks.GitHub;

        public GitHubClient(IEntityMapper mapper, IHttpHelper httpHelper)
        {
            this.mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId)
        {
           
            IList<PersonalInfoDto> personalInfo = new List<PersonalInfoDto>();
            IList<GitHubUserResponse> gitHubUserResponse = new List<GitHubUserResponse>();
            var client = httpHelper.GetConfiguredHttpClient(ConfigKeys.GitHubUserApiSearchUrl);


            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + userId);
            if (response.IsSuccessStatusCode)
            {
                var gitUserResponse = await response.Content.ReadAsAsync<GitHubUserResponse>();
                if (gitUserResponse != null)
                {
                    gitHubUserResponse.Add(gitUserResponse);
                }

            }

            personalInfo =
                mapper.Map<IList<GitHubUserResponse>, IList<PersonalInfoDto>>(gitHubUserResponse);
            return personalInfo;
        }

      
        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }
        
    }
}
