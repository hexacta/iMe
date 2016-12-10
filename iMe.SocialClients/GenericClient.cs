using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMe.Dto;
using iMe.Interfaces;

namespace iMe.SocialClients
{
    public class GenericClient : ISocialNetworkClient
    {
        
        private readonly ISocialNetworkClient[] clients;

        public GenericClient(ISocialNetworkClient[] snClients)
        {
            clients = snClients;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            List<PersonalInfoDto> returnValue = new List<PersonalInfoDto>();
            SocialNetworks selectedClient = (SocialNetworks)Enum.Parse(typeof(SocialNetworks), clientType, true);

            if (selectedClient != SocialNetworks.All)
            {

                foreach (var snClient in clients)
                {
                    if (snClient.GetSocialNetworkName() == selectedClient)
                    {
                        returnValue = (await snClient.GetPersonalInfo(userId)).ToList();
                    }   
                }
            }
            else
            {
                foreach (var snClient in clients)
                {
                    returnValue.AddRange(await snClient.GetPersonalInfo(userId));
                }
            }
            return returnValue;
        }

        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId)
        {
            throw new NotImplementedException();
        }



        public Task Login()
        {
            throw new NotImplementedException();
        }

        public SocialNetworks GetSocialNetworkName()
        {
            return SocialNetworks.Generic;
        }
    }
}
