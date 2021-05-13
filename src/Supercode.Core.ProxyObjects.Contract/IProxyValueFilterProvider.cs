using Supercode.Core.ProxyObjects.Filters;
using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects
{
    public interface IProxyValueFilterProvider
    {
        IEnumerable<IProxyValueFilter> GetAll();
    }
}
