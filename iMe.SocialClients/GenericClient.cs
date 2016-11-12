using System;
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
        private readonly IClientFactory clientFactory;


        public GenericClient(IClientFactory factory)
        {
            clientFactory = factory;
        }

        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            var client = clientFactory.GetClient(clientType);
            return client.GetPersonalInfo(userId);
        }

        public Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId)
        {
            throw new NotImplementedException();
        }

       

        public Task Login()
        {
            throw new NotImplementedException();
        }
    }
}
