﻿using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueMVC.Infrastructure.Implementation.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SignInManagerBoutique = BoutiqueMVC.Infrastructure.Implementation.Identities.SignInManagerBoutique;

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
            services.AddTransient<UserManagerBoutique>();
            services.AddTransient<IUserManagerBoutique, UserManagerBoutique>();
            services.AddTransient<IUserManagerService, UserManagerService>();
            services.AddTransient<IRoleStoreBoutique, RoleStoreBoutique>();
            services.AddTransient<IRoleStoreService, RoleStoreService>();
            services.AddTransient<ISignInManagerBoutique, SignInManagerBoutique>();
            services.AddTransient<IUserStore<BoutiqueUserEntity>, UserStoreBoutique>();
            services.AddTransient<IPasswordHasher<BoutiqueUserEntity>, PasswordHasherBoutique>();
            services.AddTransient<IUserClaimsPrincipalFactory<BoutiqueUserEntity>, UserClaimsPrincipalBoutique>();
            services.AddTransient<IUserConfirmation<BoutiqueUserEntity>, UserConfirmationBoutique>();
        }
    }
}