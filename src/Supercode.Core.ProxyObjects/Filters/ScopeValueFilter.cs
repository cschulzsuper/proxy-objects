using Supercode.Core.ProxyObjects.Exceptions;
using Supercode.Core.ProxyObjects.Filters.ScopeValues;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Supercode.Core.ProxyObjects.Filters
{
    public class ScopeValueFilter<TStack> : IProxyValueFilter
        where TStack : class, IScopeValueStack
    {
        private readonly TStack _scopeValueStack;

        public ScopeValueFilter(TStack scopeValueStack)
        {
            _scopeValueStack = scopeValueStack;
        }

        public async Task OnAccessAsync<TResult>(ProxyValueContext<TResult> context, Func<Task> next)
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

                throw new ProxyObjectsException("The type of a scoped value does not match the property type");
            }
        }
    }
}
