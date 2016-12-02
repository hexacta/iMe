using System.Collections.Generic;
using System.Threading.Tasks;

using iMe.Business;
using iMe.Dto;
using iMe.IServices;

namespace iMe.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ISocialNetworkServiceExecutor socialNetworkServiceExecutor;

        public PersonalInfoService(ISocialNetworkServiceExecutor socialNetworkServiceExecutor)
        {
            this.socialNetworkServiceExecutor = socialNetworkServiceExecutor;
        }

        public async Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId)
        {
            return await this.socialNetworkServiceExecutor.InvokeProvider<List<PersonalInfoDto>>(clientType, userId);
        }
    }
}