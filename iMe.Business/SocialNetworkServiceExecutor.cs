using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using iMe.Integration.Models;
using iMe.Interfaces;

namespace iMe.Business
{
    public class SocialNetworkServiceExecutor : ISocialNetworkServiceExecutor
    {
        private readonly ISocialNetworkServiceLocator serviceLocator;

        private readonly IEntityMapper mapper;

        public SocialNetworkServiceExecutor(ISocialNetworkServiceLocator serviceLocator, IEntityMapper mapper)
        {
            this.serviceLocator = serviceLocator;
            this.mapper = mapper;
        }

        public async Task<TResult> InvokeProvider<TResult>(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0 || string.IsNullOrEmpty(parameters[0]?.ToString()))
            {
                throw new ArgumentException(
                          "Provider invocation parameters not found. Client type name is required",
                          nameof(parameters));
            }

            // Skip async generated stack frames and resolve inmediate caller from service
            var caller = new System.Diagnostics.StackFrame(5, true).GetMethod().Name;

            var clientService = this.serviceLocator.GetInstance(parameters[0].ToString());

            var serviceResponse =
                await
                    (Task<IList<SocialClientResponse>>)
                    clientService.GetType()
                        .InvokeMember(
                            caller,
                            BindingFlags.InvokeMethod,
                            null,
                            clientService,
                            parameters.Skip(1).ToArray());

            return this.mapper.Map<IList<SocialClientResponse>, TResult>(serviceResponse);
        }
    }
}