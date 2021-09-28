using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для авторизации
    /// </summary>
    public static class IdentityServicesRegistration
    {
        /// <summary>
        /// Внедрить зависимости авторизации
        /// </summary>
        public static void RegistrationIdentityServices(IServiceCollection services)
        {
            services.AddTransient<IUserManagerBoutique, UserManagerBoutique>();
            services.AddTransient<UserManagerBoutique>();
            services.AddTransient<ISignInManagerBoutique, SignInManagerBoutique>();
            services.AddTransient<IUserStore<BoutiqueUser>, UserStoreBoutique>();
            services.AddTransient<IPasswordHasher<BoutiqueUser>, PasswordHasherBoutique>();
            services.AddTransient<IUserClaimsPrincipalFactory<BoutiqueUser>, UserClaimsPrincipalBoutique>();
            services.AddTransient<IUserConfirmation<BoutiqueUser>, UserConfirmationBoutique>();
        }
    }
}