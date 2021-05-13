using ConsoleApp.ProxyObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Supercode.Core.ProxyObjects.Filters.ScopeValues;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal sealed class HostedService : IHostedService
    {
        private readonly ILogger<HostedService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ApplicationState _applicationState;
        private readonly IScopeValueStack _scopeValueStack;

        public HostedService(
            ILogger<HostedService> logger,
            IHostApplicationLifetime appLifetime,
            ApplicationState applicationState,
            IScopeValueStack scopeValueStack)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _applicationState = applicationState;
            _scopeValueStack = scopeValueStack;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(async () => await Run());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Run()
        {
            _scopeValueStack.Push<ApplicationState, DateTimeOffset>(x => x.StartupTimestamp, DateTimeOffset.Now);
            await Task.Delay(1000);

            if (_applicationState.SayHello)
            {
                _logger.LogInformation(_applicationState.WelcomeMessage);
                await Task.Delay(1000);
            }

            _logger.LogInformation($"The application started started at {_applicationState.StartupTimestamp}");
            await Task.Delay(1000);

            _appLifetime.StopApplication();
        }
    }
}
