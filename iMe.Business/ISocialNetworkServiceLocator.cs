using System;

using iMe.Integration;

namespace iMe.Business
{
    public interface ISocialNetworkServiceLocator
    {
        ISocialNetworkService GetInstance(string clientTypeName);

        ISocialNetworkService GetInstance(Type clientType);
    }
}