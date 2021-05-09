using System;

namespace Supercode.Core.ProxyObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyKeyAttribute : Attribute
    {
        public string Key { get; }

        public PropertyKeyAttribute(string key)
        {
            Key = key;
        }

        public PropertyKeyAttribute(params string[] keyParts)
        {
            Key = string.Join(".", keyParts);
        }
    }
}
