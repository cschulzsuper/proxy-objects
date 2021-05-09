using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects
{
    public class ScopeCacheFilter : IAccessFilter
    {
        private readonly IDictionary<string, IDictionary<string,object>> _cachedSets = new Dictionary<string, IDictionary<string, object>>();
        
        public async Task OnAccessAsync<TResult>(AccessFilterContext<TResult> context, Func<Task> next)
            where TResult : notnull
        {
            var hasCachedValues = _cachedSets.TryGetValue(context.PropertyKeyPrefix,out var cachedValues);

            if (!hasCachedValues)
            {
                await next();

                if (context.ResultSet.Any())
                {
                    _cachedSets.Add(
                        context.PropertyKeyPrefix,
                        context.ResultSet.ToDictionary(
                            x => x.Key, 
                            x => (object)x.Value));
                }

                return;
            }

            foreach (var cachedValue in cachedValues!)
            {
                if (cachedValue.Value is TResult resultValue)
                {
                    context.ResultSet[cachedValue.Key] = resultValue;
                    continue;
                }

                throw new AccessFilterException("The type of a cached value does not match the property type");
            }
        }
    }
}
