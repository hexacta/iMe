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
        private readonly ISocialNetworkService[] serviceList;

        private readonly IEntityMapper mapper;

        public PersonalInfoService(ISocialNetworkService[] snServicesList, IEntityMapper mapper)
        {
            this.serviceList = snServicesList;
            this.mapper = mapper;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            IList<PersonalInfoDto> userResult = new List<PersonalInfoDto>();
            List<SocialClientResponse> serviceResponse = new List<SocialClientResponse>();

            // Todo: ver de refactorizar esta parte
            SocialNetworks selectedClient = (SocialNetworks)Enum.Parse(typeof(SocialNetworks), clientType, true);

            try
            {
                if (selectedClient != SocialNetworks.All)
                {
                    foreach (var snClient in this.serviceList)
                    {
                        if (snClient.SocialNetworkName == selectedClient)
                        {
                            serviceResponse = (await snClient.GetPersonalInfo(userId)).ToList();
                        }
                    }
                }
                else
                {
                    foreach (var snClient in this.serviceList)
                    {
                        serviceResponse.AddRange(await snClient.GetPersonalInfo(userId));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return this.mapper.Map<IList<SocialClientResponse>, IList<PersonalInfoDto>>(serviceResponse);
        }
    }
}