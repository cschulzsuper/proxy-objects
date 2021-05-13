using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects.Options
{
    public static class ProxyValueFilterDescriptorsExtensions
    {
        public static IList<ProxyValueFilterDescriptor> ConfigurationValue(this IList<ProxyValueFilterDescriptor> descriptors)
        {
            descriptors.Add(new ProxyValueFilterDescriptor(typeof(ConfigurationValueFilter)));
            return descriptors;
        }
    }
}