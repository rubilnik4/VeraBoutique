using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Параметры входа при регистрации и их корректность
    /// </summary>
    public class RegisterLoginValidation : AuthorizeValidation
    {
        public RegisterLoginValidation(string email, bool emailValid, string password, bool passwordValid,
                                       string passwordConfirm, bool passwordConfirmValid)
            :base(email, emailValid, password, passwordValid)
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