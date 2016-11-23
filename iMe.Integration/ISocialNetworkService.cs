using System.Collections.Generic;
using System.Threading.Tasks;

using iMe.Common;
using iMe.Integration.Models;

namespace iMe.Integration
{
    public interface ISocialNetworkService
    {
        #region Properties

        SocialNetworks SocialNetworkName { get; }

        #endregion

        #region Methods

        Task Login();

        Task<IList<SocialClientResponse>> GetPersonalInfo(string userId);

        Task<IList<SocialClientResponse>> GetPersonalInfo(string clientType, string userId);

        #endregion
    }
}