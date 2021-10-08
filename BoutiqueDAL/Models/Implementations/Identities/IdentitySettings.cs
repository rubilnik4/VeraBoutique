using BoutiqueCommon.Models.Common.Implementations.Identities;

namespace BoutiqueDAL.Models.Implementations.Identities
{
    /// <summary>
    /// Параметры авторизации
    /// </summary>
    public class IdentitySettings
    {
        public IdentitySettings(int passwordRequiredLength, bool passwordRequireDigit,
                                 bool signInRequireConfirmedEmail, bool userRequireUniqueEmail)
        {
            PasswordRequiredLength = passwordRequiredLength;
            PasswordRequireDigit = passwordRequireDigit;
            SignInRequireConfirmedEmail = signInRequireConfirmedEmail;
            UserRequireUniqueEmail = userRequireUniqueEmail;
        }

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int PasswordRequiredLength { get; }

        /// <summary>
        /// Наличие цифр в пароле
        /// </summary>
        public bool PasswordRequireDigit { get; }

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        public bool SignInRequireConfirmedEmail { get; }

        /// <summary>
        /// Уникальность почты
        /// </summary>
        public bool UserRequireUniqueEmail { get; }

        /// <summary>
        /// Параметры проверки паролей
        /// </summary>
        public PasswordValidateSettings ToPasswordSettings() =>
            new(PasswordRequiredLength, PasswordRequireDigit);
    }
}