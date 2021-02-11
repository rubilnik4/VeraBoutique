using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Logger;
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
        public static void RegisterCommonServices(IBoutiqueContainer container)
        {
            container.Register<IBoutiqueLogger, BoutiqueXamarinLogger>();
        }
    }
}