using System;

namespace Supercode.Core.ProxyObjects.Filters.ScopeValues
{
    public interface IScopeValue : IDisposable
    {
        string PropertyKey { get; }
        object PropertyValue { get; }
    }
}