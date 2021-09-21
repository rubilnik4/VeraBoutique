using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Проверка при регистрации
    /// </summary>
    public class RegisterValidation
    {
        public RegisterValidation(RegisterLoginViewModel registerLoginViewModel, RegisterPersonalViewModel registerPersonalViewModel)
        {
            RegisterLoginViewModel = registerLoginViewModel;
            RegisterPersonalViewModel = registerPersonalViewModel;
        }

        /// <summary>
        /// Регистрация. Имя пользователя и пароль
        /// </summary>
        public RegisterLoginViewModel RegisterLoginViewModel { get; }

        /// <summary>
        /// Регистрация. Личная информация
        /// </summary>
        public RegisterPersonalViewModel RegisterPersonalViewModel { get; }
    }
}