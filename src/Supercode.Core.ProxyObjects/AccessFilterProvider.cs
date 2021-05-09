using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Supercode.Core.ProxyObjects
{
    public class AccessFilterProvider : IAccessFilterProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICollection<Type> _accessFilters = new List<Type>();

        public AccessFilterProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal void Add(Type filterType)
        {
            _accessFilters.Add(filterType);
        }

        internal void Add<TFilter>()
            where TFilter : IAccessFilter
        {
            Add(typeof(TFilter));
        }

        public IEnumerable<IAccessFilter> GetAll()
            => _accessFilters
                .Select(accessFilter => (IAccessFilter)_serviceProvider.GetRequiredService(accessFilter));
    }
}