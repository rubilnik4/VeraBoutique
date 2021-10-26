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
    public abstract class BaseNavigationService<TParameter, TPage> : IBaseNavigationService<TParameter>
        where TParameter : BaseNavigationOptions
        where TPage : Page
    {
        protected BaseNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;


        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        public string NavigationParameterName =>
            NavigationOptionsInfo.GetNavigationParameterName<TParameter>();

        /// <summary>
        /// Имя страницы для навигации
        /// </summary>
        public string PageName =>
            typeof(TPage).Name;

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public virtual async Task<INavigationResult> NavigateTo(TParameter parameter) =>
             await new NavigationParameters
             {{ NavigationParameterName, parameter }}.
             MapAsync(navigationParameters => _navigationService.NavigateAsync(PageName, navigationParameters));

        /// <summary>
        /// Перейти назад
        /// </summary>
        public Task<INavigationResult> NavigateBack() =>
            _navigationService.GoBackAsync();
    }
}