using System;
using System.Reflection;

namespace Supercode.Core.ProxyObjects.KeySources
{
    public class AttributeKeySource<TAttribute> : IProxyValueKeySource
        where TAttribute : Attribute
    {
        private readonly Func<TAttribute, string> _selector;

        public AttributeKeySource(Func<TAttribute, string> selector)
        {
            _selector = selector;
        }

        public string? GetOrDefault(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<TAttribute>();
            return attribute != null
                ? _selector(attribute)
                : null;
        }
    }
}
