using System;
using System.Runtime.Serialization;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    public class AccessFilterContextException : ProxyObjectsException
    {
        public AccessFilterContextException()
        {
        }

        public AccessFilterContextException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public AccessFilterContextException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected AccessFilterContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
