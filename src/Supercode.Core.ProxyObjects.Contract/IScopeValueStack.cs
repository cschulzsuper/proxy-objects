using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Supercode.Core.ProxyObjects.Attributes;

namespace Supercode.Core.ProxyObjects
{
    public interface IScopeValueStack
    {
        [ErrorMessage("Could not push value")]
        IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, TProperty>> property, TProperty value)
            where TProperty : notnull;

        [ErrorMessage("Could not push list value")]
        IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, IEnumerable<TProperty>>> property, TProperty value)
            where TProperty : notnull;

        [ErrorMessage("Could not get scope value list")]
        IEnumerable<IScopeValue> GetAll();
    }
}