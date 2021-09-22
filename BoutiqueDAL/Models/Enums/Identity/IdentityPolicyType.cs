namespace BoutiqueDAL.Models.Enums.Identity
{
    /// <summary>
    /// Тип политики авторизации
    /// </summary>
    public static class IdentityPolicyType
    {
        /// <summary>
        /// Права администратора
        /// </summary>
        public const string ADMIN_POLICY = "Admin";

        /// <summary>
        /// Права пользователя
        /// </summary>
        public const string USER_POLICY = "User";
    }
}