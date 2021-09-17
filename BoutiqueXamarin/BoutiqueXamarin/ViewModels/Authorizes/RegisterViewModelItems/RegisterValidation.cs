using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Проверка при регистрации
    /// </summary>
    public class RegisterValidation
    {
        public RegisterValidation(AuthorizeValidation authorizeValidation)
        {
            AuthorizeValidation = authorizeValidation;
        }

        /// <summary>
        /// Параметры авторизации и их корректность
        /// </summary>
        public AuthorizeValidation AuthorizeValidation { get; }
    }
}