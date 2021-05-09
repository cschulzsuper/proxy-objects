using System;

namespace Supercode.Core.ProxyObjects
{
    public class ScopeValue : IScopeValue
    {
        private readonly ScopeValueStack _scopeValueStack;

        public ScopeValue(ScopeValueStack scopeValueStack, string propertyKey, object propertyValue)
        {
            _scopeValueStack = scopeValueStack;
            _scopeValueStack.Values.Add(this);

            PropertyKey = propertyKey;
            PropertyValue = propertyValue;
        }

        public void Dispose()
        {
            _scopeValueStack.Values.Remove(this);
            GC.SuppressFinalize(this);
        }

        public string PropertyKey { get; }
        public object PropertyValue { get; }
    }
}
