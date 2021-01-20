using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueXamarin.Infrastructure.Implementations;
using Prism.Ioc;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация общих сервисов
    /// </summary>
    public static class CommonServicesRegistration
    {
        /// <summary>
        /// Регистрация общих сервисов
        /// </summary>
        public static void RegisterCommonServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBoutiqueLogger, BoutiqueXamarinLogger>();
        }
    }
}