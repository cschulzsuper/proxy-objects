using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Supercode.Core.ProxyObjects
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

            proxyObjectInterceptor.ProxyPropertyValueResolver = _serviceProvider.GetRequiredService<IProxyValueResolver>();

            return proxy;
        }
    }
}
