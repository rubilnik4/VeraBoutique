using System;
using System.Security.Cryptography;
using System.Text;
using BoutiqueMVC.Factories.Database;
using BoutiqueMVC.Models.Enums.Identity;
using BoutiqueMVC.Models.Implementations.Environment;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Подключение зависимостей для авторизации
    /// </summary>
    public static class AuthServices
    {
        /// <summary>
        /// Добавить политики авторизации
        /// </summary>
        public static void AddAuthorization(IServiceCollection services) =>
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyType.Admin.ToString(), policy => policy.RequireClaim(ClaimType.Everything.ToString()));
                options.AddPolicy(PolicyType.User.ToString(), policy => policy.RequireClaim(ClaimType.ReadOnly.ToString()));
            });

        /// <summary>
        /// Внедрить зависимости авторизации
        /// </summary>
        public static void InjectJwtServices(IServiceCollection services, IConfiguration configuration) =>
            JwtSettingsFactory.GetJwtSettings(configuration).
            Map(jwtSettings => services.AddSingleton(jwtSettings).
                               Void(_ => AddJwtAuthentication(services, configuration)));

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