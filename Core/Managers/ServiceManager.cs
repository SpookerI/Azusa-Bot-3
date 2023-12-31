using System;
using Microsoft.Extensions.DependencyInjection;

namespace Azusa.bot_3.Core.Managers
{
    public static class ServiceManager
    {
        public static IServiceProvider Provider { get; set; }

        public static void SetProvider(ServiceCollection collection)
            => Provider = collection.BuildServiceProvider();
        public static T GetService<T>() where T : new ()
            => Provider.GetRequiredService<T>();
    }
}