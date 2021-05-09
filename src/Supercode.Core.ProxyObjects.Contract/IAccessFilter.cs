using System;
using System.Threading.Tasks;
using Supercode.Core.ProxyObjects.Attributes;

namespace Supercode.Core.ProxyObjects
{
    public interface IAccessFilter
    {
        [ErrorMessage("Could not execute access filter")]
        Task OnAccessAsync<TResult>(AccessFilterContext<TResult> context, Func<Task> next)
            where TResult : notnull;
    }
}
