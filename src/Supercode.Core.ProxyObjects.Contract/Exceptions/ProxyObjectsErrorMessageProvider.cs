using Supercode.Core.ProxyObjects.Filters;
using Supercode.Core.ProxyObjects.Filters.ScopeValues;
using System;
using System.Reflection;

namespace Supercode.Core.ProxyObjects.Exceptions
{
    public class ProxyObjectsErrorMessageProvider
    {
        public FormattableString? GetOrDefaultAsync(MemberInfo constraint)
        {
            var memberInfo = constraint;
            var memberInfoName = memberInfo.Name;
            var memberInfoType = memberInfo.DeclaringType?.Name;

            return (memberInfoType, memberInfoName) switch
            {
                (nameof(IProxyValueFilter), nameof(IProxyValueFilter.OnAccessAsync))
                => $"Could not execute access filter",

                (nameof(IProxyValueFilterProvider), nameof(IProxyValueFilterProvider.GetAll))
                => $"Could not get access filter list",

                (nameof(IProxyObjectFactory), nameof(IProxyObjectFactory.Create))
                => $"Could not create proxy",

                (nameof(IProxyValueResolver), nameof(IProxyValueResolver.Resolve))
                => $"Could not invoke property on target",

                (nameof(IScopeValueStack), nameof(IScopeValueStack.Push))
                => $"Could not push value",

                (nameof(IScopeValueStack), nameof(IScopeValueStack.GetAll))
                => $"Could not get scope value list",

                _ => null,
            };
        }
    }
}
