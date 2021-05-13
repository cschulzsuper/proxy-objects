using Microsoft.Extensions.DependencyInjection;
using Supercode.Core.ProxyObjects.Options;
using System;

namespace Supercode.Core.ProxyObjects.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProxyObjects<TFactory>(this IServiceCollection services, Action<ProxyObjectsOptions> configure)
            where TFactory : class, IProxyObjectFactory
        {
            services.AddScoped<IProxyObjectFactory, TFactory>();
            services.AddScoped<IProxyValueResolver, ProxyValueResolver>();
            services.AddScoped<IProxyValueFilterProvider, ProxyValueFilterProvider>();
            services.AddScoped<IProxyValueKeyProvider, ProxyValueKeyProvider>();

            services.Configure(configure);

            return services;
        }

        public static IServiceCollection AddProxyObject<TProxy>(this IServiceCollection services)
            where TProxy : class
        {
            return services
                .AddScoped(serviceProvider => serviceProvider
                    .GetRequiredService<IProxyObjectFactory>()
                    .Create<TProxy>());
        }
    }
}
