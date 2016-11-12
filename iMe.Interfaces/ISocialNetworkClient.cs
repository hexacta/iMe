using System.Collections.Generic;
using System.Threading.Tasks;
using iMe.Dto;

namespace iMe.Interfaces
{
    public interface ISocialNetworkClient
    {
        Task Login();

        Task<IList<PersonalInfoDto>> GetPersonalInfo(string userId);
    }
}