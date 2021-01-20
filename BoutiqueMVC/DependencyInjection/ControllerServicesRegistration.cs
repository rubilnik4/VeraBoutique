using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для контроллеров
    /// </summary>
    public static class ControllerServicesRegistration
    {
        /// <summary>
        /// Внедрить зависимости к базе данных
        /// </summary>
        public static void RegistrationControllerServices(IServiceCollection services)
        {
            ConverterServicesRegistration.RegisterTransferConverters(services);

            services.AddTransient<IUserManagerBoutique, UserManagerBoutique>();
            services.AddTransient<ISignInManagerBoutique, SignInManagerBoutique>();
        }
    }
}