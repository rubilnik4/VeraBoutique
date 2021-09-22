namespace BoutiqueMVC.Models.Implementations.Identity
{
    /// <summary>
    /// Параметры авторизации
    /// </summary>
    public class AuthorizeSettings
    {
        public AuthorizeSettings(int passwordRequiredLength, bool passwordRequireDigit,
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
    }
}