using System;
using iMe.Integration;

namespace iMe.Business
{
    public interface ISocialNetworkServiceLocator
    {
        ISocialNetworkProvider GetInstance(string clientTypeName);

        ISocialNetworkProvider GetInstance(Type clientType);
    }
}