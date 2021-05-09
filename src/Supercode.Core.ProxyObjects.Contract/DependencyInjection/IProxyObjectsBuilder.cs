namespace Supercode.Core.ProxyObjects.DependencyInjection
{
    public interface IProxyObjectsBuilder
    {
        IProxyObjectsBuilder AddScopeValues();
        IProxyObjectsBuilder AddScopeCache();
        IProxyObjectsBuilder AddDefaultValues();
        IProxyObjectsBuilder AddAccessFilter<TAccessFilter>()
            where TAccessFilter : IAccessFilter;
    }
}
