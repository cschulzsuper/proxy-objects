using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Supercode.Core.ProxyObjects.Filters.ScopeValues
{
    public interface IScopeValueStack
    {
        IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, TProperty>> property, TProperty value)
            where TProperty : notnull;

        IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, IEnumerable<TProperty>>> property, TProperty value)
            where TProperty : notnull;

        IEnumerable<IScopeValue> GetAll();
    }
}