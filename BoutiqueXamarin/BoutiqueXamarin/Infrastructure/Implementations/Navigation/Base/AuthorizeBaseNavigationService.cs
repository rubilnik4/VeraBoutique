using System.Threading.Tasks;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
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
        protected AuthorizeBaseNavigationService(INavigationService navigationService, ILoginStore loginStore)
            : base(navigationService)
        {
            _loginStore = loginStore;
        }

        /// <summary>
        /// Хранение и загрузка данных аутентификации
        /// </summary>
        private readonly ILoginStore _loginStore;

        public override Task NavigateTo(TParameter parameter) =>
            await ValidateToken().
            ResultErrorBindOkBadBindAsync( () )

        /// <summary>
        /// Проверка токена
        /// </summary>
        private async Task<IResultError> ValidateToken() =>
            await _loginStore.GetToken();
    }
}