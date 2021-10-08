namespace BoutiqueCommon.Models.Common.Implementations.Identities
{
    /// <summary>
    /// Параметры проверки паролей
    /// </summary>
    public class PasswordValidateSettings
    {
        public PasswordValidateSettings(int passwordRequiredLength, bool passwordRequireDigit)
        {
            PasswordRequiredLength = passwordRequiredLength;
            PasswordRequireDigit = passwordRequireDigit;
        }

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int PasswordRequiredLength { get; }

        /// <summary>
        /// Наличие цифр в пароле
        /// </summary>
        public bool PasswordRequireDigit { get; }
    }
}