using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Supercode.Core.ProxyObjects
{
    public class ConfigurationValueFilter : IAccessFilter
    {
        private readonly IConfiguration _configuration;

        public ConfigurationValueFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnAccessAsync<TResult>(AccessFilterContext<TResult> context, Func<Task> next)
            where TResult : notnull
        {
            await next();

            var configurationKeyPrefix = context.PropertyKeyPrefix.Replace(".", ":");

            var configurationSection = _configuration.GetSection(configurationKeyPrefix);
            var configurationValues = configurationSection.Exists()
                ? configurationSection.AsEnumerable()
                : Array.Empty<KeyValuePair<string, string>>();

            foreach (var (configurationKey, _) in configurationValues)
            {
                var configurationType = typeof(TResult);
                
                var propertyKey = configurationKey.Replace(":", ".");
                var propertyValue = (TResult)_configuration.GetValue(
                    configurationType, configurationKey);

                context.ResultSet[propertyKey] = propertyValue;
            }
        }
    }
}
