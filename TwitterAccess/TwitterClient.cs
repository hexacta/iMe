using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper;
using iMe.Dto;
using iMe.Interfaces;
using LinqToTwitter;


namespace TwitterAccess
{
    public class TwitterClient : ISocialNetworkClient
    {
        private static ApplicationOnlyAuthorizer _auth;

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
                Mapper.Map<IList<User>, IList<PersonalInfoDto>>(userList);
            return personalInfo;
        }
    }
}
