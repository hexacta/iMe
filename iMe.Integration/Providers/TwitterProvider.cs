using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iMe.Common;
using iMe.Integration.Models;
using iMe.Interfaces;
using LinqToTwitter;

namespace iMe.Integration.Providers
{
    public class TwitterProvider : ISocialNetworkProvider
    {
        private static ApplicationOnlyAuthorizer _auth;

        private readonly IEntityMapper mapper;

        public SocialNetworks SocialNetworkName => SocialNetworks.Twitter;

        public TwitterProvider(IEntityMapper mapper)
        {
            this.mapper = mapper;
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
                                CredentialStore =
                                    new InMemoryCredentialStore
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
        public async Task<IList<SocialClientResponse>> GetPersonalInfo(string userId)
        {
            IList<User> userList = new List<User>();
            await this.Login();
            var twitterCtx = new TwitterContext(_auth);

            try
            {
                userList =
                    await
                        (from user in twitterCtx.User
                         where user.Type == UserType.Lookup && user.ScreenNameList == userId
                         select user).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            IList<SocialClientResponse> personalInfo =
                this.mapper.Map<IList<User>, IList<SocialClientResponse>>(userList);

            return personalInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IList<SocialClientResponse>> GetPersonalInfo(string clientType, string userId)
        {
            throw new NotImplementedException();
        }
    }
}