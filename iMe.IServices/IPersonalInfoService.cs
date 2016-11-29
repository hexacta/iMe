using System;
using System.Collections.Generic;
using System.Text;
using iMe.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace iMe.IServices
{
    
        public interface IPersonalInfoService
        {
            Task<IList<PersonalInfoDto>> GetPersonalInfo(string clientType, string userId);
        }
    
}
