using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper;
using iMe.Dto;
using LinqToTwitter;
using NetworkAccess;

namespace TwitterAccess
{
    public class TwitterClient : ISocialNetworkClient
    {
        private static ApplicationOnlyAuthorizer _auth;
        private static IMapper _mapper;

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
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, PersonalInfoDto>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.UserIDResponse))
                    .ForMember(dest => dest.UserName,
                        opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.ProfilePicUrl,
                        opts => opts.MapFrom(src => src.ProfileImageUrl))
                    .ForMember(dest => dest.Bio,
                        opts => opts.MapFrom(src => src.Description));
            });

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

            IList<PersonalInfoDto> personalInfo = Mapper.Map<IList<User>, IList<PersonalInfoDto>>(userList);
            return personalInfo;
        }
    }
}
