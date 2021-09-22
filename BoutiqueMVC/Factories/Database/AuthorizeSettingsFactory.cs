using System;
using System.Configuration;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.Extensions.Configuration;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueMVC.Factories.Database
{
    /// <summary>
    /// Получение параметров авторизации
    /// </summary>
    public static class AuthorizeSettingsFactory
    {
        /// <summary>
        /// Секция в конфигурационном файле
        /// </summary>
        private const string AUTHORIZE_SECTION = "AuthorizeSettings";

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        private const string PASSWORD_REQUIRED_LENGTH = "PasswordRequiredLength";

        /// <summary>
        /// Наличие цифр в пароле
        /// </summary>
        private const string PASSWORD_REQUIRE_DIGIT = "PasswordRequireDigit";

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        private const string REQUIRE_CONFIRMED_EMAIL = "RequireConfirmedEmail";

        /// <summary>
        /// Уникальность почты
        /// </summary>
        private const string REQUIRE_UNIQUE_EMAIL = "RequireUniqueEmail";

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public static AuthorizeSettings GetAuthorizeSettings(IConfiguration configuration) =>
            new(GetPasswordRequiredLength(configuration), GetPasswordRequireDigit(configuration),
                GetRequireConfirmedEmail(configuration), GetRequireUniqueEmail(configuration));

        /// <summary>
        /// Получить необходимую длину пароля
        /// </summary>
        private static int GetPasswordRequiredLength(IConfiguration configuration) =>
            configuration[$"{AUTHORIZE_SECTION}:{PASSWORD_REQUIRED_LENGTH}"].
            WhereContinue(expires => Int32.TryParse(expires, out _),
                okFunc: Int32.Parse,
                badFunc: _ => throw new ConfigurationErrorsException());

        /// <summary>
        /// Получить необходимость цифры в пароле
        /// </summary>
        private static bool GetPasswordRequireDigit(IConfiguration configuration) =>
            configuration[$"{AUTHORIZE_SECTION}:{PASSWORD_REQUIRE_DIGIT}"].
            WhereContinue(expires => Boolean.TryParse(expires, out _),
                okFunc: Boolean.Parse,
                badFunc: _ => throw new ConfigurationErrorsException());

        /// <summary>
        /// Получить необходимость подтверждения почты
        /// </summary>
        private static bool GetRequireConfirmedEmail(IConfiguration configuration) =>
            configuration[$"{AUTHORIZE_SECTION}:{REQUIRE_CONFIRMED_EMAIL}"].
            WhereContinue(expires => Boolean.TryParse(expires, out _),
                okFunc: Boolean.Parse,
                badFunc: _ => throw new ConfigurationErrorsException());

        /// <summary>
        /// Получить необходимость уникальной почты
        /// </summary>
        private static bool GetRequireUniqueEmail(IConfiguration configuration) =>
            configuration[$"{AUTHORIZE_SECTION}:{REQUIRE_UNIQUE_EMAIL}"].
            WhereContinue(expires => Boolean.TryParse(expires, out _),
                okFunc: Boolean.Parse,
                badFunc: _ => throw new ConfigurationErrorsException());
    }
}