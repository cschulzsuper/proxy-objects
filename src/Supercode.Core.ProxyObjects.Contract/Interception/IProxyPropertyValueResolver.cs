using System.Reflection;
using Supercode.Core.ProxyObjects.Attributes;

namespace Supercode.Core.ProxyObjects.Interception
{
    public interface IProxyPropertyValueResolver
    {
        [ErrorMessage("Could not invoke property on target")]
        object Resolve(PropertyInfo targetProperty);
    }
}