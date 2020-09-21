using System.Collections.Generic;

namespace BoutiqueDAL.Models.Enums.Identity
{
    /// <summary>
    /// Типы ролей для авторизации
    /// </summary>
    public static class IdentityRoleTypes
    {
        /// <summary>
        /// Роль администратора
        /// </summary>
        public const string ADMIN_ROLE = "Admin";

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public const string USER_ROLE = "User";

        /// <summary>
        /// Список ролей
        /// </summary>
        public static IReadOnlyCollection<string> Roles =>
            new List<string>
            {
                ADMIN_ROLE,
                USER_ROLE
            }.AsReadOnly();
    }
}