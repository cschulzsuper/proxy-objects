using Castle.DynamicProxy;

namespace Supercode.Core.ProxyObjects.Interception
{
    public class DynamicProxyObjectFactory : IProxyObjectFactory
    {
        private readonly IProxyGenerator _proxyGenerator;
        private readonly IProxyPropertyValueResolver _proxyPropertyValueResolver;

        public DynamicProxyObjectFactory(IProxyGenerator proxyGenerator, IProxyPropertyValueResolver proxyPropertyValueResolver)
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
