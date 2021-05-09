using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Supercode.Core.ProxyObjects.Interception
{
    public class DispatchProxyObjectFactory : IProxyObjectFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DispatchProxyObjectFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TProxy Create<TProxy>()
            where TProxy : class
        {
            var proxy = DispatchProxy.Create<TProxy, DispatchProxyPropertyValueInterceptor>();
            var proxyObject = (object)proxy!;
            var proxyObjectInterceptor = (DispatchProxyPropertyValueInterceptor)proxyObject;

            proxyObjectInterceptor.ProxyPropertyValueResolver = _serviceProvider.GetRequiredService<IProxyPropertyValueResolver>();

            return proxy;
        }
    }
}
