using System;
using System.Linq;
using System.Threading.Tasks;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects
{
    public class ScopeValueFilter : IAccessFilter
    {
        private readonly IScopeValueStack _scopeValueStack;

        public ScopeValueFilter(IScopeValueStack scopeValueStack)
        {
            _scopeValueStack = scopeValueStack;
        }

        public async Task OnAccessAsync<TResult>(AccessFilterContext<TResult> context, Func<Task> next)
            where TResult : notnull
        {
            await next();

            var stackValues = _scopeValueStack
                .GetAll()
                .Where(x => x.PropertyKey.StartsWith(context.PropertyKeyPrefix));

            foreach (var stackValue in stackValues)
            {
                if (stackValue.PropertyValue is TResult resultValue)
                {
                    context.ResultSet[stackValue.PropertyKey] = resultValue;
                    continue;
                }

                throw new AccessFilterException("The type of a scoped value does not match the property type");
            }
        }
    }
}
