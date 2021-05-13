using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    [Serializable]
    public class ProxyObjectsException : Exception
    {
        [IgnoreDataMember]
        public FormattableString UnformattedMessage { get; }

        public ProxyObjectsException()
        {
            UnformattedMessage = FormattableStringFactory.Create(string.Empty);
        }

        public ProxyObjectsException(FormattableString message)
            : base(message.ToString())
        {
            UnformattedMessage = message;
        }

        public ProxyObjectsException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
            UnformattedMessage = FormattableStringFactory.Create(message, parameters);
        }

        public ProxyObjectsException(string message, Exception inner, params object[] parameters)
            : base(string.Format(message, parameters), inner)
        {
            UnformattedMessage = FormattableStringFactory.Create(message, parameters);
        }

        protected ProxyObjectsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            UnformattedMessage = FormattableStringFactory.Create(string.Empty);
        }
    }
}
