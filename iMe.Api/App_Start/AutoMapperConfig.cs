using AutoMapper;
using iMe.Dto;
using iMe.SocialClients.Models;
using LinqToTwitter;

namespace iMe
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.AddProfile<PersonalInfoDtoProfile>();
                }
            );
        }
    }

    public class PersonalInfoDtoProfile : Profile
    {
        public PersonalInfoDtoProfile()
        {
            CreateMap<User, PersonalInfoDto>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.UserIDResponse))
                    .ForMember(dest => dest.UserName,
                        opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.ProfilePicUrl,
                        opts => opts.MapFrom(src => src.ProfileImageUrl))
                    .ForMember(dest => dest.Bio,
                        opts => opts.MapFrom(src => src.Description));

            CreateMap<GitHubUserResponse, PersonalInfoDto>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.UserName,
                        opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.ProfilePicUrl,
                        opts => opts.MapFrom(src => src.Avatar_Url))
                    .ForMember(dest => dest.Bio,
                        opts => opts.MapFrom(src => src.Bio));
        }
    }
}