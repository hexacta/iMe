using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iMe.Interfaces;
using iMe.Dto;
using LinqToTwitter;
using iMe.Integration.Models;

namespace iMe.Mapper
{

    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper mapper;

        public EntityMapper()
        {
            var config = new MapperConfiguration(ConfigureMaps);
            mapper = config.CreateMapper();
        }


        private void ConfigureMaps(IMapperConfigurationExpression config)
        {
            config.AddProfile<PersonalInfoProfile>();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }
    }

    public class PersonalInfoProfile : Profile
    {
        public PersonalInfoProfile()
        {
            CreateMap<User, SocialClientResponse>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.UserIDResponse))
                    .ForMember(dest => dest.UserName,
                        opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.ProfilePicUrl,
                        opts => opts.MapFrom(src => src.ProfileImageUrl))
                    .ForMember(dest => dest.Bio,
                        opts => opts.MapFrom(src => src.Description));

            CreateMap<GitHubUserResponse, SocialClientResponse>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.UserName,
                        opts => opts.MapFrom(src => src.Name))
                        .ForMember(dest => dest.ProfilePicUrl,
                        opts => opts.MapFrom(src => src.Avatar_Url))
                    .ForMember(dest => dest.Bio,
                        opts => opts.MapFrom(src => src.Bio));

            CreateMap<SocialClientResponse, PersonalInfoDto>();
        }
    }
}
    
