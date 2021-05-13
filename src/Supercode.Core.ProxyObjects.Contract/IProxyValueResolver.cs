using System.Reflection;

namespace Supercode.Core.ProxyObjects
{
    public interface IProxyValueResolver
    {
        object Resolve(PropertyInfo targetProperty);
    }
}