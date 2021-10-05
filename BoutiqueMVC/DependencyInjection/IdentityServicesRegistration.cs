using BoutiqueDAL.Infrastructure.Implementations.Identity;
using BoutiqueDAL.Infrastructure.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueMVC.Infrastructure.Implementation.Identity;
using BoutiqueMVC.Infrastructure.Interfaces.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SignInManagerBoutique = BoutiqueMVC.Infrastructure.Implementation.Identity.SignInManagerBoutique;

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
            services.AddTransient<IRoleStoreBoutique, RoleStoreBoutique>();
            services.AddTransient<UserManagerBoutique>();
            services.AddTransient<ISignInManagerBoutique, SignInManagerBoutique>();
            services.AddTransient<IUserStore<BoutiqueIdentityUser>, UserStoreBoutique>();
            services.AddTransient<IPasswordHasher<BoutiqueIdentityUser>, PasswordHasherBoutique>();
            services.AddTransient<IUserClaimsPrincipalFactory<BoutiqueIdentityUser>, UserClaimsPrincipalBoutique>();
            services.AddTransient<IUserConfirmation<BoutiqueIdentityUser>, UserConfirmationBoutique>();
        }
    }
}