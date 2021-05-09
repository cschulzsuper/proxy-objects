using System;
using System.Runtime.Serialization;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    public class AccessFilterException : ProxyObjectsException
    {
        public AccessFilterException()
        {
        }

        public AccessFilterException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public AccessFilterException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected AccessFilterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
