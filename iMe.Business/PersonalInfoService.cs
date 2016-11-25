using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using iMe.Common;
using iMe.Dto;
using iMe.Integration;
using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Business
{
    public class PersonalInfoService : ISocialService
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