using System.Reflection;

namespace Supercode.Core.ProxyObjects.KeySources
{
    public interface IProxyValueKeySource
    {
        public string? GetOrDefault(PropertyInfo propertyInfo);
    }
}