using System.Threading.Tasks;

namespace iMe.Business
{
    public interface ISocialNetworkServiceExecutor
    {
        Task<TResult> InvokeProvider<TResult>(params object[] parameters);
    }
}