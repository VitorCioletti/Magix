namespace Magix.DependencyInjection.Setup
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Service;
    using Service.Interface;

    public static class Setup
    {
        public static IServiceProvider ApplyDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IMatchService, MatchService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
