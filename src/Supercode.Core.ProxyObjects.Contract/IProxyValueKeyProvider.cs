using System.Reflection;

namespace Supercode.Core.ProxyObjects
{
    public interface IProxyValueKeyProvider
    {
        public string Get(PropertyInfo propertyInfo);
    }
}
