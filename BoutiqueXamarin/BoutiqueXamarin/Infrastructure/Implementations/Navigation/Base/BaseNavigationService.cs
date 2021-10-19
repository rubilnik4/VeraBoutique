using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using ResultFunctional.FunctionalExtensions.Async;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base
{
    /// <summary>
    /// Базовый сервис навигации
    /// </summary>
    public abstract class BaseNavigationService<TParameter, TPage> : BackNavigationService, IBaseNavigationService<TParameter>
        where TParameter : BaseNavigationParameters
        where TPage : Page
    {
        protected BaseNavigationService(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации Prism
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        public string NavigationParameterName =>
            NavigationParametersInfo.GetNavigationParameterName<TParameter>();

        /// <summary>
        /// Имя страницы для навигации
        /// </summary>
        public string PageName =>
            typeof(TPage).Name;

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public virtual async Task NavigateTo(TParameter parameter) =>
             await new NavigationParameters
             {{ NavigationParameterName, parameter }}.
             MapAsync(navigationParameters => _navigationService.NavigateAsync(PageName, navigationParameters));
    }
}