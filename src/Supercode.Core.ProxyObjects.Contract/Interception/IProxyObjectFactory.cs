using Supercode.Core.ProxyObjects.Attributes;

namespace Supercode.Core.ProxyObjects.Interception
{
    public interface IProxyObjectFactory
    {
        [ErrorMessage("Could not create proxy")]
        TProxy Create<TProxy>()
            where TProxy : class;
    }
}