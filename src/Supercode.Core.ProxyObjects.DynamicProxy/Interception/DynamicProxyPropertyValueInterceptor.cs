using System.Linq;
using Castle.DynamicProxy;

namespace Supercode.Core.ProxyObjects.Interception
{
    public class DynamicProxyPropertyValueInterceptor : IInterceptor
    {
        private readonly IProxyPropertyValueResolver _proxyPropertyValueResolver;

        public DynamicProxyPropertyValueInterceptor(IProxyPropertyValueResolver proxyPropertyValueResolver)
        {
            _proxyPropertyValueResolver = proxyPropertyValueResolver;
        }

        public void Intercept(IInvocation invocation)
        {
            var targetObject = invocation.Proxy;
            var targetMethod = invocation.Method;

            var proxyObjectType = invocation.TargetType;

            var propertyName = targetMethod.Name.Split('_')[1];
            var propertyInfo = proxyObjectType
                .GetProperties()
                .Single(p => p.Name == propertyName);

            invocation.ReturnValue = _proxyPropertyValueResolver.Resolve(propertyInfo);
        }
    }
}
