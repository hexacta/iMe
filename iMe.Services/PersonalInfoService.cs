using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using iMe.Dto;
using iMe.Integration.Models;
using iMe.IServices;
using iMe.Interfaces;
using iMe.Business;

namespace iMe.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ISocialNetworkServiceLocator serviceLocator;

        private readonly IEntityMapper mapper;

        public PersonalInfoService(ISocialNetworkServiceLocator serviceLocator, IEntityMapper mapper)
        {
            this.serviceLocator = serviceLocator;
            this.mapper = mapper;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            List<SocialClientResponse> serviceResponse = new List<SocialClientResponse>();
            var clientService = this.serviceLocator.GetInstance(clientType);

            try
            {
                serviceResponse.AddRange(await clientService.GetPersonalInfo(userId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return this.mapper.Map<IList<SocialClientResponse>, IList<PersonalInfoDto>>(serviceResponse);
        }
    }
}