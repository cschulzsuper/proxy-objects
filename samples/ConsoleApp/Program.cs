using Castle.DynamicProxy;
using ConsoleApp.ProxyObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supercode.Core.ProxyObjects;
using Supercode.Core.ProxyObjects.Attributes;
using Supercode.Core.ProxyObjects.DependencyInjection;
using Supercode.Core.ProxyObjects.Filters.ScopeValues;
using Supercode.Core.ProxyObjects.Options;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(c => c.AddJsonFile("appsettings.json"))
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<HostedService>();

                    services.AddSingleton<IProxyGenerator, ProxyGenerator>();
                    services.AddScoped<IScopeValueStack, ScopeValueStack>();

                    services.AddProxyObjects<DynamicProxyObjectFactory>(options =>
                    {
                        options.KeySources.AttributeKey<ProxyValueKeyAttribute>(x => x.Key);

                        options.Filters.ScopeValue<IScopeValueStack>();
                        options.Filters.ScopeCache();
                        options.Filters.ConfigurationValue();
                        options.Filters.DefaultValue();
                    });

                    services.AddProxyObject<ApplicationState>();
                })
                .RunConsoleAsync();
        }
    }
}
