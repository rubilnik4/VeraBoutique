using System;

namespace BoutiqueDAL.Models.Implementations.Connection
{
    /// <summary>
    /// Параметры авторизации
    /// </summary>
    public class Authorization : IEquatable<Authorization>
    {
        public Authorization(string username, string password)
        {
            if (!IsUsernameValid(username)) throw new ArgumentNullException(nameof(username));
            if (!IsPasswordValid(password)) throw new ArgumentNullException(nameof(password));

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

        #region IEquatable
        public override bool Equals(object? obj) => obj is Authorization authorization && Equals(authorization);

        public bool Equals(Authorization? other) =>
            other?.Username == Username &&
            other?.Password == Password;

        public override int GetHashCode() => HashCode.Combine(Username, Password);
        #endregion
    }
}