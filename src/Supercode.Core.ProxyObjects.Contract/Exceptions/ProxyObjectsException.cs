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

        public ProxyObjectsException(FormattableString message, Exception inner)
            : base(message.ToString(), inner)
        {
            UnformattedMessage = message;
        }

        protected ProxyObjectsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            UnformattedMessage = FormattableStringFactory.Create(string.Empty);
        }
    }
}
