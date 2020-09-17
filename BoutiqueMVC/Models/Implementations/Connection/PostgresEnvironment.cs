namespace BoutiqueMVC.Models.Implementations.Connection
{
    /// <summary>
    /// Параметры подключения postgres в окружении
    /// </summary>
    public static class PostgresEnvironment
    {
        /// <summary>
        /// Сервер
        /// </summary>
        public const string HOST_NAME = "POSTGRES_HOST";

        /// <summary>
        /// Порт
        /// </summary>
        public const string PORT_NAME = "POSTGRES_PORT";

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public const string DATABASE_NAME = "POSTGRES_DB";

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public const string USER_NAME = "POSTGRES_USER";

        /// <summary>
        /// Пароль
        /// </summary>
        public const string PASSWORD_NAME = "POSTGRES_PASSWORD";

        /// <summary>
        /// JWT ключ
        /// </summary>
        public const string JWT_KEY = "JWT_KEY";
    }
}