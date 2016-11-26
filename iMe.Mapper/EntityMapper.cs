using AutoMapper;

using iMe.Dto;
using iMe.Integration.Models;
using iMe.Interfaces;
using LinqToTwitter;

namespace iMe.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper mapper;

        public EntityMapper()
        {
            var config = new MapperConfiguration(ConfigureMaps);
            this.mapper = config.CreateMapper();
        }

        private static void ConfigureMaps(IMapperConfigurationExpression config)
        {
            config.AddProfile<PersonalInfoProfile>();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return this.mapper.Map<TSource, TDestination>(source);
        }
    }

    public class PersonalInfoProfile : Profile
    {
        public PersonalInfoProfile()
        {
            this.CreateMap<User, SocialClientResponse>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserIDResponse))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProfilePicUrl, opts => opts.MapFrom(src => src.ProfileImageUrl))
                .ForMember(dest => dest.Bio, opts => opts.MapFrom(src => src.Description)).MaxDepth(1);

            this.CreateMap<GitHubUserResponse, SocialClientResponse>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProfilePicUrl, opts => opts.MapFrom(src => src.Avatar_Url))
                .ForMember(dest => dest.Bio, opts => opts.MapFrom(src => src.Bio)).MaxDepth(1);

            this.CreateMap<SocialClientResponse, PersonalInfoDto>().MaxDepth(1);
        }
    }
}