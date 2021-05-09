using System;

namespace Supercode.Core.ProxyObjects
{
    public interface IScopeValue : IDisposable   
    {
        string PropertyKey { get; }
        object PropertyValue { get; }
    }
}