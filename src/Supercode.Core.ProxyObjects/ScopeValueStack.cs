using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Supercode.Core.ProxyObjects.Attributes;
using Supercode.Core.ProxyObjects.Exceptions;

namespace Supercode.Core.ProxyObjects
{
    public class ScopeValueStack : IScopeValueStack
    {
        internal readonly ICollection<IScopeValue> Values = new List<IScopeValue>();

        public IEnumerable<IScopeValue> GetAll() => Values;

        public IScopeValue Push<TProxyObject, TProperty>(Expression<Func<TProxyObject, TProperty>> property, TProperty value)
            where TProperty : notnull
        {
            var (propertyInfo, propertyKeyAttribute) = ExtractExpressionInfo(property);

            if (typeof(TProperty) != propertyInfo.PropertyType)
            {
                throw new ScopeValueStackException($"Property is not of type {typeof(TProperty).Name}");
            }

            var propertyKey = propertyKeyAttribute.Key;

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
            var (propertyInfo, propertyKeyAttribute) = ExtractExpressionInfo(property);

            if (typeof(IEnumerable<TProperty>) != propertyInfo.PropertyType)
            {
                throw new ScopeValueStackException($"Property is not of type {typeof(IEnumerable<TProperty>).Name}");
            }

            var propertyKey = $"{propertyKeyAttribute.Key}.{Guid.NewGuid()}";

            if (typeof(TProxyObject).IsGenericType &&
                typeof(TProxyObject).GenericTypeArguments.Length == 1)
            {
                propertyKey = propertyKey.Replace(".T.", $".{typeof(TProxyObject).GenericTypeArguments.Single().Name}.");
            }
            
            return new ScopeValue(this, propertyKey, value);
        }

        private static (PropertyInfo, PropertyKeyAttribute) ExtractExpressionInfo(Expression property)
        {
            if (property is not LambdaExpression lambdaExpression)
            {
                throw new ScopeValueStackException("Expression is not a lambda expression");
            }

            if (lambdaExpression.Body is not MemberExpression memberExpression)
            {
                throw new ScopeValueStackException("Expression is not a member expression");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ScopeValueStackException("Expression is not a property expression");
            }

            var propertyKeyAttribute = propertyInfo.GetCustomAttribute<PropertyKeyAttribute>();
            if (propertyKeyAttribute == null)
            {
                throw new ScopeValueStackException($"Property has no {nameof(PropertyKeyAttribute)}");
            }

            return (propertyInfo, propertyKeyAttribute);
        }
    }
}
