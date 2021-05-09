using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects
{
    public class DefaultValueFilter : IAccessFilter
    {
        public async Task OnAccessAsync<TResult>(AccessFilterContext<TResult> context, Func<Task> next)
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

            throw new AccessFilterException("The type of the default value does not match the property type");
        }
    }
}
