using Supercode.Core.ProxyObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Supercode.Core.ProxyObjects.Filters.ScopeValues
{
    public class ScopeValueStack : IScopeValueStack
    {
        private readonly IProxyValueKeyProvider _proxyValueKeyProvider;

        public ScopeValueStack(IProxyValueKeyProvider proxyValueKeyProvider)
        {
            _proxyValueKeyProvider = proxyValueKeyProvider;
        }

        internal readonly ICollection<IScopeValue> Values = new List<IScopeValue>();

        public IEnumerable<IScopeValue> GetAll() => Values;

        public IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, TProperty>> property, TProperty value)
            where TProperty : notnull
        {
            var (propertyInfo, propertyKey) = ExtractExpressionInfo(property);

            if (typeof(TProperty) != propertyInfo.PropertyType)
            {
                throw new ProxyObjectsException($"Property is not of type {typeof(TProperty).Name}");
            }

            if (typeof(TProxyObject).IsGenericType &&
                typeof(TProxyObject).GenericTypeArguments.Length == 1)
            {
                propertyKey = propertyKey.Replace(".T.", $".{typeof(TProxyObject).GenericTypeArguments.Single().Name}.");
            }

            return new ScopeValue(this, propertyKey, value);
        }

        public IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, IEnumerable<TProperty>>> property, TProperty value)
           where TProperty : notnull
        {
            var (propertyInfo, propertyKey) = ExtractExpressionInfo(property);

            if (typeof(IEnumerable<TProperty>) != propertyInfo.PropertyType)
            {
                throw new ProxyObjectsException($"Property is not of type {typeof(IEnumerable<TProperty>).Name}");
            }

            propertyKey = $"{propertyKey}.{Guid.NewGuid()}";

            if (typeof(TProxyObject).IsGenericType &&
                typeof(TProxyObject).GenericTypeArguments.Length == 1)
            {
                propertyKey = propertyKey.Replace(".T.", $".{typeof(TProxyObject).GenericTypeArguments.Single().Name}.");
            }

            return new ScopeValue(this, propertyKey, value);
        }

        private (PropertyInfo, string) ExtractExpressionInfo(Expression property)
        {
            if (property is not LambdaExpression lambdaExpression)
            {
                throw new ProxyObjectsException($"Expression is not a lambda expression");
            }

            if (lambdaExpression.Body is not MemberExpression memberExpression)
            {
                throw new ProxyObjectsException($"Expression is not a member expression");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ProxyObjectsException($"Expression is not a property expression");
            }

            var propertyKeyAttribute = _proxyValueKeyProvider.Get(propertyInfo);
            return (propertyInfo, propertyKeyAttribute);
        }
    }
}
