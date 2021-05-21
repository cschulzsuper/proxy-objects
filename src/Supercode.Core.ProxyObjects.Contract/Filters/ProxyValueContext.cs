using Supercode.Core.ProxyObjects.Exceptions;
using System.Collections.Generic;
using System.Reflection;

namespace Supercode.Core.ProxyObjects.Filters
{
    public record ProxyValueContext<TResult>(PropertyInfo Property, string PropertyKeyPrefix, string PropertyName)
        where TResult : notnull
    {
        public TResult Result
        {
            get => ResultSet.ContainsKey(PropertyKeyPrefix)
                ? ResultSet[PropertyKeyPrefix]
                : throw new ProxyObjectsException($"Result value for property '{Property.PropertyType.Name}.{Property.Name}' is not available");

            set => ResultSet[PropertyKeyPrefix] = value 
                ?? throw new ProxyObjectsException($"Result value for property '{Property.PropertyType.Name}.{Property.Name}' can not be null");
        }

        public IDictionary<string, TResult> ResultSet { get; } = new Dictionary<string, TResult>();
    }
}