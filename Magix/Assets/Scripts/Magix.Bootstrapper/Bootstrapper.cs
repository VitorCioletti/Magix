namespace Magix.Bootstrapper
{
    using System;
    using DependencyInjection;
    using DependencyInjection.Setup;

    public static class Bootstrapper
    {
        public static void Boot()
        {
            _configureDependencyInjection();
        }

        private static void _configureDependencyInjection()
        {
            IServiceProvider serviceProvider = Setup.ApplyDependencyInjection();

            Resolver.SetServiceProvider(serviceProvider);
        }
    }
}
