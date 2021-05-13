using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects.Options
{
    public class ProxyObjectsOptions
    {
        public IList<ProxyValueKeySourceDescriptor> KeySources { get; } = new List<ProxyValueKeySourceDescriptor>();
        public IList<ProxyValueFilterDescriptor> Filters { get; } = new List<ProxyValueFilterDescriptor>();
    }
}