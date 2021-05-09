using System.Collections.Generic;
using Supercode.Core.ProxyObjects.Attributes;

namespace Supercode.Core.ProxyObjects
{
    public interface IAccessFilterProvider
    {
        [ErrorMessage("Could not get access filter list")]
        IEnumerable<IAccessFilter> GetAll();
    }
}
