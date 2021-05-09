using System.Collections.Generic;
using System.Reflection;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects
{
    public record AccessFilterContext<TResult>(PropertyInfo Property, string PropertyKeyPrefix, string PropertyName)
        where TResult : notnull
    {
        public TResult Result
        {
            get => ResultSet.ContainsKey(PropertyKeyPrefix) 
                ? ResultSet[PropertyKeyPrefix]
                : throw new AccessFilterContextException("Result value is not available");

            set => ResultSet[PropertyKeyPrefix] = value ?? throw new AccessFilterContextException("Value can not be null");
        }

        public IDictionary<string,TResult> ResultSet { get; } = new Dictionary<string, TResult>();
    }
}