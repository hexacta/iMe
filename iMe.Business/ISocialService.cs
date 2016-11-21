using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMe.Dto;

namespace iMe.Business
{
    public interface ISocialService
    {
        Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId);
    }
}
