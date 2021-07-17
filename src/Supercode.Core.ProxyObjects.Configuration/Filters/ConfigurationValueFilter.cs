using Microsoft.Extensions.Configuration;
using Supercode.Core.ProxyObjects.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supercode.Core.ProxyObjects
{
    public class ConfigurationValueFilter : IProxyValueFilter
    {
        private readonly IConfiguration _configuration;

        public ConfigurationValueFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnAccessAsync<TResult>(ProxyValueContext<TResult> context, Func<Task> next)
            where TResult : notnull
        {
            await next();

            var configurationKeyPrefix = context.PropertyKeyPrefix.Replace(".", ":");
            var configurationType = typeof(TResult);

            var configurationSection = _configuration.GetSection(configurationKeyPrefix);
            var configurationValues = configurationSection.Exists()
                ? configurationSection.AsEnumerable()
                : Array.Empty<KeyValuePair<string, string>>();

            foreach (var (configurationKey, _) in configurationValues)
            {
                if (configurationKey == configurationKeyPrefix &&
                    context.Property.PropertyType != typeof(string) &&
                    context.Property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
                {
                    continue;
                }
                
                var propertyKey = configurationKey.Replace(":", ".");
                var propertyValue = (TResult)_configuration.GetSection(configurationKey).Get(configurationType);
                if (propertyValue != null)
                {
                    context.ResultSet[propertyKey] = propertyValue;
                }
            }
        }
    }
}
