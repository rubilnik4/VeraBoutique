using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
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

        /// <summary>
        /// Модель регистрации
        /// </summary>
        public IRegisterDomain Register =>
           new RegisterDomain(Authorize, Personal);

        /// <summary>
        /// Модель авторизации
        /// </summary>
        public IAuthorizeDomain Authorize =>
            new AuthorizeDomain(RegisterLoginViewModel.Login, RegisterLoginViewModel.Password);

        /// <summary>
        /// Модель личных данных
        /// </summary>
        public IPersonalDomain Personal =>
            new PersonalDomain(RegisterPersonalViewModel.Name, RegisterPersonalViewModel.Surname,
                               RegisterPersonalViewModel.Address, RegisterPersonalViewModel.Phone);
    }
}