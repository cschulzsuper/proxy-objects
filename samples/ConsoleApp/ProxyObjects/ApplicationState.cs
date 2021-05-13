using Supercode.Core.ProxyObjects.Attributes;
using System;
using System.ComponentModel;

namespace ConsoleApp.ProxyObjects
{
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
}
