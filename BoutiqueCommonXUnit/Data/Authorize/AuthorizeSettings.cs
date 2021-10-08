using BoutiqueCommon.Models.Common.Implementations.Identities;

namespace BoutiqueCommonXUnit.Data.Authorize
{
    /// <summary>
    /// Параметры авторизации
    /// </summary>
    public static class AuthorizeSettings
    {
        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public static PasswordValidateSettings PasswordSettings =>
            new(8, true);
    }
}