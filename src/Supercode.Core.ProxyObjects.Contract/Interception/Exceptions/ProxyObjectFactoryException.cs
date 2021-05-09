using System;
using System.Runtime.Serialization;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects.Interception.Exceptions
{
    public class ProxyObjectFactoryException : ProxyObjectsException
    {
        public ProxyObjectFactoryException()
        {
        }

        public ProxyObjectFactoryException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public ProxyObjectFactoryException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected ProxyObjectFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
