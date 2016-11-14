using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iMe.Dto;
using iMe.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using iMe.SocialClients.Models;
using LinqToTwitter;

namespace iMe.SocialClients
{
    public class GitHubClient : ISocialNetworkClient
    {
        private readonly IEntityMapper mapper;

        public GitHubClient(IEntityMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId)
        {
           
            IList<PersonalInfoDto> personalInfo = new List<PersonalInfoDto>();
            IList<GitHubUserResponse> gitHubUserResponse = new List<GitHubUserResponse>();
            var client = GetHttpClient("http://api.github.com/users/");


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

        private HttpClient GetHttpClient(string requestUrl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(requestUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                //github pide UserAgent
            return client;
        }

        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public SocialNetworks GetSocialNetworkName()
        {
            return SocialNetworks.GitHub;
        }
    }
}
