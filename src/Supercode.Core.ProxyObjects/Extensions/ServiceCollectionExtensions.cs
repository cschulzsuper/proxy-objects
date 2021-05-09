using System;
using Supercode.Core.ProxyObjects.DependencyInjection;
using Supercode.Core.ProxyObjects.Interception;
using Microsoft.Extensions.DependencyInjection;

namespace Supercode.Core.ProxyObjects.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProxyObjects<TFactory>(this IServiceCollection services, Action<IProxyObjectsBuilder> config)
            where TFactory : class, IProxyObjectFactory
        {
            var builder = new ProxyObjectsBuilder();
            config.Invoke(builder);

            if (builder.EnableScopeValues)
            {
                services.AddScoped<IScopeValueStack, ScopeValueStack>();
                services.AddScoped<ScopeValueFilter>();
            }

            if (builder.EnableScopeCache)
            {
                services.AddScoped<ScopeCacheFilter>();
            }

            foreach (var accessFilterType in builder.AccessFilterTypes)
            {
                services.AddScoped(accessFilterType);
            }

            if (builder.EnableDefaultValues)
            {
                services.AddScoped<DefaultValueFilter>();
            }

            return services
                .AddScoped<IProxyObjectFactory, TFactory>()
                .AddScoped<IProxyPropertyValueResolver, ProxyPropertyValueResolver>()
                .AddScoped<IAccessFilterProvider>(serviceProvider =>
                    {
                        var accessFilterProvider = new AccessFilterProvider(serviceProvider);

                        if (builder.EnableScopeValues)
                        {
                            accessFilterProvider.Add<ScopeValueFilter>();
                        }

                        if (builder.EnableScopeCache)
                        {
                            accessFilterProvider.Add<ScopeCacheFilter>();
                        }

                        foreach (var accessFilterType in builder.AccessFilterTypes)
                        {
                            accessFilterProvider.Add(accessFilterType);
                        }

                        if (builder.EnableDefaultValues)
                        {
                            accessFilterProvider.Add<DefaultValueFilter>();
                        }

                        return accessFilterProvider;
                    });
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
