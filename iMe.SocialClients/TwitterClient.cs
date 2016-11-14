using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper;
using iMe.Dto;
using iMe.SocialClients;
using LinqToTwitter;
using iMe.Interfaces;

namespace iMe.SocialClients
{
    public class TwitterClient : ISocialNetworkClient
    {
        private static ApplicationOnlyAuthorizer _auth;
        private readonly IEntityMapper mapper;
        
        public TwitterClient(IEntityMapper mapper)
        {
            this.mapper=mapper;
        }
        
        /// <summary>
        /// Twitter Login
        /// </summary>
        public async Task Login()
        {
            try
            {
                _auth = new ApplicationOnlyAuthorizer
                {
                    CredentialStore = new InMemoryCredentialStore
                    {
                        ConsumerKey = ConfigKeys.ConsumerKey,
                        ConsumerSecret = ConfigKeys.ConsumerSecret
                    }
                };
                await _auth.AuthorizeAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve bio de twitter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId)
        {
            IList<User> userList = new List<User>();
            await Login();
            var twitterCtx = new TwitterContext(_auth);

            try
            {
                userList = await
                   (from user in twitterCtx.User
                    where user.Type == UserType.Lookup &&
                          user.ScreenNameList == userId
                    select user).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            IList<PersonalInfoDto> personalInfo = 
                mapper.Map<IList<User>, IList<PersonalInfoDto>>(userList);
            return personalInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            throw new NotImplementedException();
        }

        public SocialNetworks GetSocialNetworkName()
        {
            return SocialNetworks.Twitter;
        }
    }
}
