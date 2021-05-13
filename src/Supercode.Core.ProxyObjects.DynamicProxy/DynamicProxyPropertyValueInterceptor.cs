using Castle.DynamicProxy;
using System.Linq;

namespace Supercode.Core.ProxyObjects
{
    public class DynamicProxyPropertyValueInterceptor : IInterceptor
    {
        private readonly IProxyValueResolver _proxyPropertyValueResolver;

        public DynamicProxyPropertyValueInterceptor(IProxyValueResolver proxyPropertyValueResolver)
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
