using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMe.Integration;
using iMe.Integration.Models;
using iMe.Common;

namespace iMe.Business
{
    public class GenericService : ISocialNetworkService
    {
        
        private readonly ISocialNetworkService[] _services;

        public SocialNetworks SocialNetworkName => SocialNetworks.Generic;

        public GenericService(ISocialNetworkService[] snServices)
        {
            _services = snServices;
        }

        public async Task<IList<SocialClientResponse>> GetPersonalInfo(string clientType, string userId)
        {
            List<SocialClientResponse> returnValue = new List<SocialClientResponse>();

            //Todo: ver de refactorizar esta parte
            SocialNetworks selectedClient = (SocialNetworks)Enum.Parse(typeof(SocialNetworks), clientType, true);

            if (selectedClient != SocialNetworks.All)
            {

                foreach (var snClient in _services)
                {
                    if (snClient.SocialNetworkName == selectedClient)
                    {
                        returnValue = (await snClient.GetPersonalInfo(userId)).ToList();
                    }   
                }
            }
            else
            {
                foreach (var snClient in _services)
                {
                    returnValue.AddRange(await snClient.GetPersonalInfo(userId));
                }
            }
            return returnValue;
        }

        public Task<IList<SocialClientResponse>> GetPersonalInfo(string userId)
        {
            throw new NotImplementedException();
        }



        public Task Login()
        {
            throw new NotImplementedException();
        }

        
    }
}
