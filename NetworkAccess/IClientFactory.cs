using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMe.Interfaces;

namespace iMe.Interfaces
{
    public interface IClientFactory
    {
        ISocialNetworkClient GetClient(string clientType);
    }
}
