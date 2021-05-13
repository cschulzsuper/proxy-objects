using Castle.DynamicProxy;

namespace Supercode.Core.ProxyObjects
{
    public class DynamicProxyObjectFactory : IProxyObjectFactory
    {
        private readonly IProxyGenerator _proxyGenerator;
        private readonly IProxyValueResolver _proxyPropertyValueResolver;

        public DynamicProxyObjectFactory(IProxyGenerator proxyGenerator, IProxyValueResolver proxyPropertyValueResolver)
        {
            _proxyGenerator = proxyGenerator;
            _proxyPropertyValueResolver = proxyPropertyValueResolver;
        }

        public TProxy Create<TProxy>()
            where TProxy : class
        {
            var dynamicProxyValueInterceptor = new DynamicProxyPropertyValueInterceptor(_proxyPropertyValueResolver);
            var dynamicProxy = !typeof(TProxy).IsInterface
                ? _proxyGenerator.CreateClassProxy<TProxy>(dynamicProxyValueInterceptor)
                : _proxyGenerator.CreateInterfaceProxyWithoutTarget<TProxy>(dynamicProxyValueInterceptor);

            return dynamicProxy;
        }
    }
}
