using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Supercode.Core.ProxyObjects.Exceptions;
using Supercode.Core.ProxyObjects.KeySources;
using Supercode.Core.ProxyObjects.Options;
using System;
using System.Linq;
using System.Reflection;

namespace Supercode.Core.ProxyObjects
{
    public class ProxyValueKeyProvider : IProxyValueKeyProvider
    {
        private readonly IOptions<ProxyObjectsOptions> _proxyObjectsOptions;
        private readonly IServiceProvider _serviceProvider;

        public ProxyValueKeyProvider(
            IOptions<ProxyObjectsOptions> proxyObjectsOptions,
            IServiceProvider serviceProvider)
        {
            _proxyObjectsOptions = proxyObjectsOptions;
            _serviceProvider = serviceProvider;
        }

        public string Get(PropertyInfo propertyInfo)
        {
            var keySourceDescriptors = _proxyObjectsOptions.Value.KeySources;

            foreach (var keySourceDescriptor in keySourceDescriptors)
            {
                var keySourceType = keySourceDescriptor.Type;
                var keySourceArguments = keySourceDescriptor.Arguments;
                var keySource = null as IProxyValueKeySource;

                if (!keySourceDescriptor.Arguments.Any())
                {
                    keySource = GetServiceOrDefault(keySourceType);
                }

                keySource ??= CreateInstanceOrDefault(keySourceType, keySourceArguments);

                var key = keySource?.GetOrDefault(propertyInfo);
                if (key != null)
                {
                    return key;
                }
            }

            var propertyName = propertyInfo.Name;
            var declaringTypeName = propertyInfo.DeclaringType!.Name;

            throw new ProxyObjectsException($"Property {propertyName} of type {declaringTypeName} has no key");
        }

        public IProxyValueKeySource? CreateInstanceOrDefault(Type type, object[] arguments)
        {
            if (type.IsInterface)
            {
                return null;
            }

            return ActivatorUtilities.CreateInstance(_serviceProvider, type, arguments) as IProxyValueKeySource;
        }

        public IProxyValueKeySource? GetServiceOrDefault(Type proxyValueKeySourceType)
        {
            return _serviceProvider.GetService(proxyValueKeySourceType) as IProxyValueKeySource;
        }
    }
}
