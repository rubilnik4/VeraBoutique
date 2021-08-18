using System;
using System.Reactive.Linq;
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
            _token = AuthorizeCommand.ToProperty(this, nameof(Token));
            _authorize = this.WhenAnyValue(x => x.Login, x => x.Password).
                                       Select(x =>(IAuthorizeDomain) new AuthorizeDomain(x.Item1, x.Item2)).
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

        private readonly ObservableAsPropertyHelper<IAuthorizeDomain> _authorize;

        public IAuthorizeDomain Authorize =>
            _authorize.Value;

        private readonly ObservableAsPropertyHelper<IResultValue<string>> _token;

        public IResultValue<string> Token =>
            _token.Value;

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<IAuthorizeDomain, IResultValue<string>> AuthorizeCommand { get; }
    }
}