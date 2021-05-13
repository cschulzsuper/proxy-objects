using Supercode.Core.ProxyObjects.Filters;
using Supercode.Core.ProxyObjects.Filters.ScopeValues;
using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects.Options
{
    public static class ProxyValueFilterDescriptorsExtensions
    {
        public static IList<ProxyValueFilterDescriptor> ScopeValue<TStack>(this IList<ProxyValueFilterDescriptor> descriptors)
            where TStack : class, IScopeValueStack
        {
            descriptors.Add(new ProxyValueFilterDescriptor(typeof(ScopeValueFilter<TStack>)));
            return descriptors;
        }

        public static IList<ProxyValueFilterDescriptor> ScopeCache(this IList<ProxyValueFilterDescriptor> descriptors)
        {
            descriptors.Add(new ProxyValueFilterDescriptor(typeof(ScopeCacheFilter)));
            return descriptors;
        }

        public static IList<ProxyValueFilterDescriptor> DefaultValue(this IList<ProxyValueFilterDescriptor> descriptors)
        {
            descriptors.Add(new ProxyValueFilterDescriptor(typeof(DefaultValueFilter)));
            return descriptors;
        }
    }
}
