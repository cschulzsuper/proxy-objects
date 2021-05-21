# Proxy Objects
A proxy object injection for the .NET dependency injection.


[![Build](https://img.shields.io/github/workflow/status/cschulzsuper/proxy-objects/Deploy%20Master)](https://github.com/cschulzsuper/proxy-objects/actions?query=workflow%3A"Deploy+Master")
[![Nuget](https://img.shields.io/github/v/release/cschulzsuper/proxy-objects?sort=semver)](https://github.com/cschulzsuper/proxy-objects/packages/)

## Getting Started
Once the first release is ready, it will be available on [Nuget](https://www.nuget.org/).  
You can download a preview version [here](https://github.com/cschulzsuper/proxy-objects/packages/).

## Demo

A demo will become available in the future.

## Overview

This library makes it possible to inject proxy objects into your services.
The proxies will give access to configuration values, scope values or values you provide via a custom filter.

### Add Proxy Object

A proxy object is simple class with annotated property definitions.
For this example all properties must have a getter and must be virtual.

```csharp

public class ApplicationState
{
    [ProxyValueKey("ApplicationState.StartupTimestamp")]
    public virtual DateTimeOffset StartupTimestamp { get; }

    [ProxyValueKey("ApplicationState.WelcomeMessage")]
    public virtual string WelcomeMessage { get; }

    [DefaultValue(true)]
    [ProxyValueKey("ApplicationState.SayHello")]
    public virtual bool SayHello { get; }
}

```

### Add Service

The following service registration is a basic example.

```csharp

// This is the registration of the proxy object factory, with proxy value sources
services.AddProxyObjects<DynamicProxyObjectFactory>(options =>
{
    // A key source specifies how the key of a proxy value (property) is determined
    options.KeySources.AttributeKey<ProxyValueKeyAttribute>(x => x.Key);

    // Filters will be executed in order when determining a proxy value

    // Use values defined via IScopeValueStack as a values source
    options.Filters.ScopeValue<IScopeValueStack>();

    // Use cached values as value source
    options.Filters.ScopeCache();

    // Use IConfiguration as value source
    options.Filters.ConfigurationValue();

    // Use the DefaultValueAttribute as value source
    options.Filters.DefaultValue();
});

// This is the proxy object registration
services.AddProxyObject<ApplicationState>();

// The ScopeValueStack is needed in order to use the scope values
services.AddScoped<IScopeValueStack, ScopeValueStack>();

// The DynamicProxyObjectFactory use Castle.DynamicProxy 
services.AddSingleton<IProxyGenerator, ProxyGenerator>();

```

# Inject Proxy Object

Inject the object as you do with every other injection.

```csharp
public class YourService
{
    private readonly ApplicationState _applicationState;

    public YourService(ApplicationState applicationState)
    {
        _applicationState = applicationState;
    }

    /* removed for brevity */
}
```

# Use Scoped Values

Override a proxy value with a scoped value.

```csharp
using var scope = _scopeValueStack.Push<ApplicationState, DateTimeOffset>(x => x.StartupTimestamp, DateTimeOffset.Now) 
{
    /* removed for brevity */

    // This can also be moved to an inner service, where the proxy object is injection instead.
    _logger.LogInformation($"The application started started at {_applicationState.StartupTimestamp}");

    /* removed for brevity */
}

```

The scopes implement `IDisposible`. Once a scoped value is disposed, it will be gone from the stack.