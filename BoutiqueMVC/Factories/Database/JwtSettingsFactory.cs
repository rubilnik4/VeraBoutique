using System;
using System.Configuration;
using System.Text;
using BoutiqueMVC.Models.Implementations.Environment;
using BoutiqueMVC.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Sync;
using Microsoft.Extensions.Configuration;

namespace BoutiqueMVC.Factories.Database
{
    /// <summary>
    /// Фабрика для создания параметров JWT токена
    /// </summary>
    public static class JwtSettingsFactory
    {
        /// <summary>
        /// Секция в конфигурационном файле
        /// </summary>
        private const string JWT_SECTION = "JwtToken";

        /// <summary>
        /// Поставщик токена
        /// </summary>
        private const string ISSUER = "Issuer";

        /// <summary>
        /// Потребитель токена
        /// </summary>
        private const string AUDIENCE = "Audience";

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public static JwtSettings GetJwtSettings(IConfiguration configuration) =>
            new JwtSettings(GetIssuerConfiguration(configuration),
                            GetAudienceConfiguration(configuration),
                            SymmetricKey);

        /// <summary>
        /// Получить потребителя токена
        /// </summary>
        private static string GetIssuerConfiguration(IConfiguration configuration) =>
            configuration[$"{JWT_SECTION}:{ISSUER}"] ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Получить поставщика токена
        /// </summary>
        private static string GetAudienceConfiguration(IConfiguration configuration) =>
            configuration[$"{JWT_SECTION}:{AUDIENCE}"] ??
            throw new ConfigurationErrorsException();

        /// <summary>
        /// Получить симметричный ключ
        /// </summary>
        private static byte[] SymmetricKey =>
            Encoding.ASCII.GetBytes(KeyEnvironment);

        /// <summary>
        /// Ключ токена в строковом значении из параметров окружения
        /// </summary>
        private static string KeyEnvironment =>
            Environment.GetEnvironmentVariable(JwtEnvironment.JWT_KEY) ??
            throw new ConfigurationErrorsException(nameof(KeyEnvironment));
    }
}