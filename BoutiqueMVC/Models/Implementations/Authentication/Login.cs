namespace BoutiqueMVC.Models.Implementations.Authentication
{
    /// <summary>
    /// Имя пользователя и пароль
    /// </summary>
    public class Login
    {
        public Login(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName{ get; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; }
    }
}