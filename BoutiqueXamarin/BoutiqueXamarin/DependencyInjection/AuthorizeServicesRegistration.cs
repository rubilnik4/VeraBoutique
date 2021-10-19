using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Logger;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация сервисов авторизации
    /// </summary>
    public static class AuthorizeServicesRegistration
    {
        /// <summary>
        /// Регистрация общих сервисов
        /// </summary>
        public static void RegisterAuthorizeServices(IBoutiqueContainer container)
        {
            container.Register<ILoginStore, LoginStore>();
            container.Register<ILoginService, LoginService>();
        }
    }
}