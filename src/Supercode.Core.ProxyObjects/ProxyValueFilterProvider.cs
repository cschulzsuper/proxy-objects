using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Supercode.Core.ProxyObjects.Filters;
using Supercode.Core.ProxyObjects.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Supercode.Core.ProxyObjects
{
    public class ProxyValueFilterProvider : IProxyValueFilterProvider
    {
        private readonly IOptions<ProxyObjectsOptions> _proxyObjectsOptions;
        private readonly IServiceProvider _serviceProvider;

        public ProxyValueFilterProvider(
            IOptions<ProxyObjectsOptions> proxyObjectsOptions,
            IServiceProvider serviceProvider)
        {
            _proxyObjectsOptions = proxyObjectsOptions;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IProxyValueFilter> GetAll()
        {
            var proxyValueFilterDescriptors = _proxyObjectsOptions.Value.Filters;

            foreach (var proxyValueFilterDescriptor in proxyValueFilterDescriptors)
            {
                var proxyValueFilterType = proxyValueFilterDescriptor.Type;
                var proxyValueFilterArguments = proxyValueFilterDescriptor.Arguments;
                var proxyValueFilter = null as IProxyValueFilter;

                if (!proxyValueFilterDescriptor.Arguments.Any())
                {
                    proxyValueFilter = GetServiceOrDefault(proxyValueFilterType);
                }

                proxyValueFilter ??= CreateInstanceOrDefault(proxyValueFilterType, proxyValueFilterArguments);

                if (proxyValueFilter != null)
                {
                    yield return proxyValueFilter;
                }
            }
        }

        public IProxyValueFilter? CreateInstanceOrDefault(Type type, object[] arguments)
        {
            if (type.IsInterface)
            {
                return null;
            }

            return ActivatorUtilities.CreateInstance(_serviceProvider, type, arguments) as IProxyValueFilter;
        }

        public IProxyValueFilter? GetServiceOrDefault(Type proxyValueFilterType)
        {
            return _serviceProvider.GetService(proxyValueFilterType) as IProxyValueFilter;
        }
    }
}