using System;

namespace BoutiqueCommon.Models.Implementation.Connection
{
    /// <summary>
    /// Параметры авторизации
    /// </summary>
    public class Authorization
    {
        public Authorization(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            Username = username;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Проверка имени пользователя
        /// </summary>
        public static bool IsUsernameValid(string? username) => !String.IsNullOrWhiteSpace(username);

        /// <summary>
        /// Проверка пароля
        /// </summary>
        public static bool IsPasswordValid(string? password) => !String.IsNullOrWhiteSpace(password);
    }
}