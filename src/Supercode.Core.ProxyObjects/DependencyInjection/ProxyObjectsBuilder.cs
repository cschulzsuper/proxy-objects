using System;
using System.Collections.Generic;

namespace Supercode.Core.ProxyObjects.DependencyInjection
{
    public class ProxyObjectsBuilder : IProxyObjectsBuilder
    {
        internal bool EnableScopeValues { get; private set; }

        internal bool EnableScopeCache { get; private set; }

        internal bool EnableDefaultValues { get; private set; }

        internal ICollection<Type> AccessFilterTypes { get; } = new List<Type>();

        public IProxyObjectsBuilder AddScopeValues()
        {
            EnableScopeValues = true;
            return this;
        }

        public IProxyObjectsBuilder AddScopeCache()
        {
            EnableScopeCache = true;
            return this;
        }

        public IProxyObjectsBuilder AddDefaultValues() 
        {
            EnableDefaultValues = true;
            return this;
        }

        public IProxyObjectsBuilder AddAccessFilter<TAccessFilter>()
            where TAccessFilter : IAccessFilter
        {
            AccessFilterTypes.Add(typeof(TAccessFilter));
            return this;
        }
    }
}