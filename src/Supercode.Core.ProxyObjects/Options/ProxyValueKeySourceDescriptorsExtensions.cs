using Supercode.Core.ProxyObjects.KeySources;
using System;
using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects.Options
{
    public static class ProxyValueKeySourceDescriptorsExtensions
    {
        public static IList<ProxyValueKeySourceDescriptor> AttributeKey<TAttribute>(this IList<ProxyValueKeySourceDescriptor> descriptors, Func<TAttribute, string> selector)
            where TAttribute : Attribute
        {
            descriptors.Add(new ProxyValueKeySourceDescriptor(typeof(AttributeKeySource<TAttribute>), selector));
            return descriptors;
        }
    }
}
