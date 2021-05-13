using System;

namespace Supercode.Core.ProxyObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ProxyValueKeyAttribute : Attribute
    {
        public string Key { get; }

        public ProxyValueKeyAttribute(string key)
        {
            Key = key;
        }

        public ProxyValueKeyAttribute(params string[] keyParts)
        {
            Key = string.Join(".", keyParts);
        }
    }
}
