using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueMVC.Factories.Database;
using BoutiqueMVC.Models.Enums.Identity;
using BoutiqueMVC.Models.Implementations.Environment;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static BoutiqueDAL.Models.Enums.Identity.IdentityRoleTypes;
using static BoutiqueMVC.Models.Enums.Identity.IdentityPolicyType;

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
                options.AddPolicy(ADMIN_POLICY, policy => policy.RequireClaim(ClaimTypes.Role, ADMIN_ROLE));
                options.AddPolicy(USER_POLICY, policy => policy.RequireClaim(ClaimTypes.Role, USER_ROLE));
            });

        /// <summary>
        /// Внедрить зависимости авторизации
        /// </summary>
        public static void RegisterJwtServices(IServiceCollection services, IConfiguration configuration) =>
            JwtSettingsFactory.GetJwtSettings(configuration).
            Map(jwtSettings => services.AddSingleton(jwtSettings).
                               Void(_ => AddJwtAuthentication(services, configuration)));

        /// <summary>
        /// Подключить сервисы авторизации к базе
        /// </summary>
        public static void RegisterDatabaseIdentities(IServiceCollection services) =>
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = IdentitySettings.PASSWORD_MINLENGTH;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                }).
                AddEntityFrameworkStores<BoutiqueEntityDatabase>().
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