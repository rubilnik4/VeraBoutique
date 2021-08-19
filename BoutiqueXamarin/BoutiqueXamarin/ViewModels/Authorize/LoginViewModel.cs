using System;
using System.Reactive.Linq;
using Akavache;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using Functional.Models.Interfaces.Result;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Authorize
{
    /// <summary>
    /// Модель авторизации
    /// </summary>
    public class LoginViewModel : NavigationBaseViewModel<LoginNavigationParameters, ILoginNavigationService>
    {
        public LoginViewModel(ILoginNavigationService loginNavigationService, IAuthorizeRestService authorizeRestService)
          : base(loginNavigationService, loginNavigationService)
        {
            AuthorizeCommand = ReactiveCommand.CreateFromTask<IAuthorizeDomain, IResultValue<string>>(authorizeRestService.AuthorizeJwt);

            _authorize = this.WhenAnyValue(x => x.Login, x => x.Password, (login, password) => (Login: login, Password: password)).
                              Select(x => (IAuthorizeDomain)new AuthorizeDomain(x.Login, x.Password)).
                              ToProperty(this, nameof(Authorize));
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string _login = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string _password = String.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IAuthorizeDomain> _authorize;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        public IAuthorizeDomain Authorize =>
            _authorize.Value;

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<IAuthorizeDomain, IResultValue<string>> AuthorizeCommand { get; }
    }
}