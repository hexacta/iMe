﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iMe.Dto;
using iMe.Interfaces;

namespace iMe.Business
{
    public class GenericClient : ISocialNetworkClient
    {
        
        private readonly ISocialNetworkClient[] clients;

        public SocialNetworks SocialNetworkName => SocialNetworks.Generic;

        public GenericClient(ISocialNetworkClient[] snClients)
        {
            clients = snClients;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            List<PersonalInfoDto> returnValue = new List<PersonalInfoDto>();

            //Todo: ver de refactorizar esta parte
            SocialNetworks selectedClient = (SocialNetworks)Enum.Parse(typeof(SocialNetworks), clientType, true);

            if (selectedClient != SocialNetworks.All)
            {

                foreach (var snClient in clients)
                {
                    if (snClient.SocialNetworkName == selectedClient)
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

        
    }
}
