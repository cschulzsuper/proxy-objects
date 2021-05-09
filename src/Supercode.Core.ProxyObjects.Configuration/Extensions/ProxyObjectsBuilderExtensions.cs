using Supercode.Core.ProxyObjects.DependencyInjection;

namespace Supercode.Core.ProxyObjects.Extensions
{
    public static class ProxyObjectsBuilderExtensions
    {
        public static IProxyObjectsBuilder AddConfigurationValues(this IProxyObjectsBuilder builder)
            => builder.AddAccessFilter<ConfigurationValueFilter>();
    }
}