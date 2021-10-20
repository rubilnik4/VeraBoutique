using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base
{
    /// <summary>
    /// Базовый сервис навигации с авторизацией
    /// </summary>
    public abstract class AuthorizeBaseNavigationService<TParameter, TPage> : BaseNavigationService<TParameter, TPage>
        where TParameter : BaseNavigationParameters
        where TPage : Page
    {
        protected AuthorizeBaseNavigationService(INavigationService navigationService, ILoginStore loginStore, 
                                                 ILoginNavigationService loginNavigationService)
            : base(navigationService)
        {
            _loginStore = loginStore;
            _loginNavigationService = loginNavigationService;
        }

        /// <summary>
        /// Хранение и загрузка данных аутентификации
        /// </summary>
        private readonly ILoginStore _loginStore;

        /// <summary>
        /// Сервис навигации к странице авторизации
        /// </summary>
        private readonly ILoginNavigationService _loginNavigationService;

        /// <summary>
        /// Перейти к странице с проверкой авторизации
        /// </summary>
        public override async Task<INavigationResult> NavigateTo(TParameter parameter) =>
            await ValidateToken().
            WhereContinueBindAsync(result => result.OkStatus,
                                   _ => base.NavigateTo(parameter),
                                   _ => _loginNavigationService.NavigateTo());

        /// <summary>
        /// Проверка токена
        /// </summary>
        private async Task<IResultValue<string>> ValidateToken() =>
            await _loginStore.GetToken();
    }
}