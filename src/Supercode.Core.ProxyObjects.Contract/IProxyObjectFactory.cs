namespace Supercode.Core.ProxyObjects
{
    public interface IProxyObjectFactory
    {
        TProxy Create<TProxy>()
            where TProxy : class;
    }
}