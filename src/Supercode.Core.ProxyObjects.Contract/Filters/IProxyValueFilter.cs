using System;
using System.Threading.Tasks;

namespace Supercode.Core.ProxyObjects.Filters
{
    public interface IProxyValueFilter
    {
        Task OnAccessAsync<TResult>(ProxyValueContext<TResult> context, Func<Task> next)
            where TResult : notnull;
    }
}
