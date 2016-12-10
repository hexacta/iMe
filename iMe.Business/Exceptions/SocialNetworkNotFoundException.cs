using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMe.Business.Exceptions
{
    /// <summary>
    /// Mostrar errores friendly, pero internamente loguear la exception posta con algun logger Log4Net o NLog
    /// </summary>
    public class SocialNetworkNotFoundException : Exception
    {
        private string v;

        public SocialNetworkNotFoundException()
        {
        }

        public SocialNetworkNotFoundException(string message)
        : base(message)
    {
        }

        public SocialNetworkNotFoundException(string message, string v) : this(message)
        {
            this.v = v;
        }

        public SocialNetworkNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
