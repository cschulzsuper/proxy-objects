using System;
using System.Runtime.Serialization;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    public class ScopeValueStackException : ProxyObjectsException
    {
        public ScopeValueStackException()
        {
        }

        public ScopeValueStackException(string message, params object[] parameters)
            : base(message, parameters)
        {
        }

        public ScopeValueStackException(string message, Exception inner, params object[] parameters)
            : base(message, inner, parameters)
        {
        }

        protected ScopeValueStackException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
