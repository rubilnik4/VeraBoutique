using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Параметры входа при регистрации и их корректность
    /// </summary>
    public class RegisterLoginValidation : AuthorizeValidation
    {
        public RegisterLoginValidation(string login, bool loginValid, string password, bool passwordValid,
                                       string passwordConfirm, bool passwordConfirmValid)
            :base(login, loginValid, password, passwordValid)
        {
            PasswordConfirm = passwordConfirm;
            PasswordConfirmValid = passwordConfirmValid;
        }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string PasswordConfirm { get; }

        /// <summary>
        /// Корректность подтверждения пароля
        /// </summary>
        public bool PasswordConfirmValid { get; }
    }
}