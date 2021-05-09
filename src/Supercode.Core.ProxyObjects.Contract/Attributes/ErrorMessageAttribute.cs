using System;

namespace Supercode.Core.ProxyObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ErrorMessageAttribute : Attribute
    {
        public string Message { get; }

        public ErrorMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
