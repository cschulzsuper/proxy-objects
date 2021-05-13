using Supercode.Core.ProxyObjects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Supercode.Core.ProxyObjects
{
    public class ProxyValueResolver : IProxyValueResolver
    {
        private readonly IProxyValueFilterProvider _proxyValueFilterProvider;
        private readonly IProxyValueKeyProvider _proxyValueKeyProvider;

        public ProxyValueResolver(
            IProxyValueFilterProvider proxyValueFilterProvider,
            IProxyValueKeyProvider proxyValueKeyProvider)
        {
            _proxyValueFilterProvider = proxyValueFilterProvider;
            _proxyValueKeyProvider = proxyValueKeyProvider;
        }

        public object Resolve(PropertyInfo targetProperty)
        {
            var propertyKey = GetPropertyKey(targetProperty);
            var propertyType = targetProperty.PropertyType;

            var accessFilterGenericType = GetAccessFilterGenericType(propertyType);
            var accessFilterContextType = typeof(ProxyValueContext<>).MakeGenericType(accessFilterGenericType);
            var accessFilterContext = (dynamic)Activator.CreateInstance(accessFilterContextType,
                targetProperty, propertyKey, targetProperty.Name)!;

            var accessFilterTask = Task.CompletedTask;
            var accessFilters = _proxyValueFilterProvider.GetAll()
                .Reverse();

            foreach (var accessFilter in accessFilters)
            {
                accessFilterTask = ExecuteAccessFilterAsync(
                    accessFilter,
                    accessFilterContext,
                    accessFilterTask);
            }

            return propertyType switch
            {
                _ when IsAsyncEnumerable(propertyType)
                    => GetEnumerableResultAsync(accessFilterContext, accessFilterTask),

                _ when IsTask(propertyType)
                    => GetResultAsync(accessFilterContext, accessFilterTask),

                _ when IsEnumerable(propertyType)
                    => GetEnumerableResult(accessFilterContext, accessFilterTask),

                _ => GetResult(accessFilterContext, accessFilterTask)
            };
        }

        private static bool IsAsyncEnumerable(Type type)
        {
            return type.IsInterface &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>)

                   || type
                       .GetInterfaces()
                       .Any(x => x.IsGenericType &&
                                 x.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>));
        }

        private static bool IsEnumerable(Type type)
        {
            return type != typeof(string) && (

                type.IsInterface &&
                type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>)

                || type
                    .GetInterfaces()
                    .Any(x => x.IsGenericType &&
                              x.GetGenericTypeDefinition() == typeof(IEnumerable<>)));
        }

        private static bool IsTask(Type type)
        {
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Task<>);
        }

        private static Task ExecuteAccessFilterAsync<TResult>(IProxyValueFilter accessFilter, ProxyValueContext<TResult> accessFilterContext, Task nextAccessFilterTask)
            where TResult : notnull
        {
            return accessFilter.OnAccessAsync(accessFilterContext, () => nextAccessFilterTask);
        }

        private static TResult GetResult<TResult>(ProxyValueContext<TResult> accessFilterContext, Task accessFilterTask)
            where TResult : notnull
        {
            accessFilterTask.Wait();
            return accessFilterContext.Result!;
        }

        private static async Task<TResult> GetResultAsync<TResult>(ProxyValueContext<TResult> accessFilterContext, Task accessFilterTask)
            where TResult : notnull
        {
            await accessFilterTask;
            return accessFilterContext.Result!;
        }

        private static IEnumerable<TResult> GetEnumerableResult<TResult>(ProxyValueContext<TResult> accessFilterContext, Task accessFilterTask)
            where TResult : notnull
        {
            accessFilterTask.Wait();
            return accessFilterContext.ResultSet.Values;
        }

        private static async IAsyncEnumerable<TResult> GetEnumerableResultAsync<TResult>(ProxyValueContext<TResult> accessFilterContext, Task accessFilterTask)
            where TResult : notnull
        {
            await accessFilterTask;

            foreach (var c in accessFilterContext.ResultSet.Values)
            {
                yield return c;
            }
        }

        private string GetPropertyKey(PropertyInfo property)
        {
            var propertyKey = _proxyValueKeyProvider.Get(property);
            var declaringType = property.DeclaringType!;

            if (declaringType.IsGenericType)
            {
                var genericArguments = declaringType
                    .GetGenericTypeDefinition()
                    .GetGenericArguments();

                foreach (var genericArgument in genericArguments)
                {
                    propertyKey = propertyKey.Replace(
                        $".{genericArgument.Name}.",
                        $".{declaringType.GenericTypeArguments.Single().Name}.");

                }
            }

            return propertyKey;
        }

        private static Type GetAccessFilterGenericType(Type propertyType)
        {
            return propertyType switch
            {
                _ when IsAsyncEnumerable(propertyType)
                    => propertyType
                        .GetGenericArguments()
                        .First(),

                _ when IsTask(propertyType)
                    => propertyType
                        .GetGenericArguments()
                        .First(),

                _ when IsEnumerable(propertyType)
                    => propertyType
                        .GetGenericArguments()
                        .First(),

                _ => propertyType
            };
        }
    }
}
