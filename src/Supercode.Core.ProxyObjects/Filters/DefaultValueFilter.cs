using Supercode.Core.ProxyObjects.Exceptions;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ProxyObjects.Filters
{
    public class DefaultValueFilter : IProxyValueFilter
    {
        public async Task OnAccessAsync<TResult>(ProxyValueContext<TResult> context, Func<Task> next)
            where TResult : notnull
        {
            await next();

            var defaultValueAttribute = context.Property.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttribute == null)
            {
                return;
            }

            if (defaultValueAttribute.Value is TResult resultValue)
            {
                context.Result = resultValue;
                return;
            }

            throw new ProxyObjectsException("The type of the default value does not match the property type");
        }
    }
}
