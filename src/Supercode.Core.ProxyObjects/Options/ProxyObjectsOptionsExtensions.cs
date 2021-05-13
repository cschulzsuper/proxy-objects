using Supercode.Core.ProxyObjects.Filters;

namespace Supercode.Core.ProxyObjects.Options
{
    public static class ProxyObjectsOptionsExtensions
    {
        public static ProxyObjectsOptions ProxyValueFilter<TFilter>(this ProxyObjectsOptions options)
            where TFilter : class, IProxyValueFilter
        {
            options.Filters.Add(new ProxyValueFilterDescriptor(typeof(TFilter)));
            return options;
        }
    }
}