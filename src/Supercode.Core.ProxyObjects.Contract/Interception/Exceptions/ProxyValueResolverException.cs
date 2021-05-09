using System;
using System.Runtime.Serialization;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects.Interception.Exceptions
{
    public class ProxyValueResolverException : ProxyObjectsException
    {
        public ProxyValueResolverException()
        {
        }

        public ProxyValueResolverException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public ProxyValueResolverException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected ProxyValueResolverException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
