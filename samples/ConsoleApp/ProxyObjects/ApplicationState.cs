using Supercode.Core.ProxyObjects.Attributes;
using System;

namespace ConsoleApp.ProxyObjects
{
    public class ApplicationState
    {
        [ProxyValueKey("ApplicationState.StartupTimestamp")]
        public virtual DateTimeOffset StartupTimestamp { get; }
    }
}
