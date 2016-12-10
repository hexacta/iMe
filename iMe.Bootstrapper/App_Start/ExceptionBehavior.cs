using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace iMe.Bootstrapper
{
    internal class ExceptionBehavior : IInterceptionBehavior
    {
        public bool WillExecute => true;

        public IMethodReturn Invoke(IMethodInvocation input,
            GetNextInterceptionBehaviorDelegate getNext)
        {
            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                WriteLog($"Method {input.MethodBase} threw exception {result.Exception.Message} at {DateTime.Now.ToLongTimeString()}",
                    result.Exception);
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        private static void WriteLog(string message, Exception invocationException)
        {
            Console.WriteLine($"{message}{Environment.NewLine}\t{invocationException.TargetSite}");
        }
    }
}