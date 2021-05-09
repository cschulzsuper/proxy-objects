using System.Linq;
using System.Reflection;

namespace Supercode.Core.ProxyObjects.Interception
{
    public class DispatchProxyPropertyValueInterceptor : DispatchProxy
    {
        internal IProxyPropertyValueResolver ProxyPropertyValueResolver { get; set; } = null!;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? _)
        {
            var proxyObjectType = targetMethod!.DeclaringType!;

            var propertyName = targetMethod.Name.Split('_')[1];
            var propertyInfo = proxyObjectType
                .GetProperties()
                .Single(p => p.Name == propertyName);

            return ProxyPropertyValueResolver.Resolve(propertyInfo);
        }
    }
}
