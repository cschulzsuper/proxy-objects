using System;
using System.Runtime.Serialization;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    public class AccessFilterProviderException : ProxyObjectsException
    {
        public AccessFilterProviderException()
        {
        }

        public AccessFilterProviderException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public AccessFilterProviderException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected AccessFilterProviderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
