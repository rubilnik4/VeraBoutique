using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueMVC.Factories.Database;
using BoutiqueMVC.Infrastructure.Implementation.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using BoutiqueMVC.Models.Implementations.Environment;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Подключение зависимостей для авторизации
    /// </summary>
    public static class AuthServicesRegistration
    {
        /// <summary>
        /// Добавить политики авторизации
        /// </summary>
        public static void AddAuthorization(IServiceCollection services) =>
            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityPolicyType.ADMIN_POLICY,
                                  policy => policy.RequireClaim(ClaimTypes.Role, IdentityRoleType.Admin.ToString()));
                options.AddPolicy(IdentityPolicyType.USER_POLICY,
                                  policy => policy.RequireClaim(ClaimTypes.Role, IdentityRoleType.User.ToString()));
            });

        /// <summary>
        /// Внедрить зависимости свойств авторизации
        /// </summary>
        public static void RegisterJwtServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(JwtSettingsFactory.GetJwtSettings(configuration));
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            AddJwtAuthentication(services, configuration);
        }

        /// <summary>
        /// Внедрить зависимости свойств регистрации
        /// </summary>
        public static void RegisterAuthServices(IServiceCollection services, IConfiguration configuration) =>
            AuthorizeSettingsFactory.GetAuthorizeSettings(configuration).
            Map(services.AddSingleton);


        /// <summary>
        /// Подключить сервисы авторизации к базе
        /// </summary>
        public static void RegisterDatabaseIdentities(IServiceCollection services, IConfiguration configuration) =>
            services.AddIdentity<BoutiqueUserEntity, IdentityRole>(options =>
                AuthorizeSettingsFactory.GetAuthorizeSettings(configuration).
                Void(authSettings => options.Password.RequiredLength = authSettings.PasswordRequiredLength).
                Void(authSettings => options.Password.RequireDigit = authSettings.PasswordRequireDigit).
                Void(authSettings => options.SignIn.RequireConfirmedEmail = authSettings.SignInRequireConfirmedEmail).
                Void(authSettings => options.User.RequireUniqueEmail = authSettings.UserRequireUniqueEmail)).
            AddEntityFrameworkStores<BoutiqueDatabase>().
            AddDefaultTokenProviders();

        /// <summary>
        /// Добавить авторизацию через JWT токен
        /// </summary>
        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration) =>
            services.
            AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options =>
                options.TokenValidationParameters = JwtSettingsFactory.GetJwtSettings(configuration).TokenValidationParameters);
    }
}