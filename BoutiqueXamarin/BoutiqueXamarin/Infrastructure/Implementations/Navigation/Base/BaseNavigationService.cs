using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Functional.FunctionalExtensions.Async;
using Prism.Navigation;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base
{
    /// <summary>
    /// Базовый сервис навигации
    /// </summary>
    public abstract class BaseNavigationService<TParameter, TPage>: IBaseNavigationService<TParameter> 
        where TParameter : EmptyNavigationParameters
        where TPage : ContentPage
    {
        protected BaseNavigationService(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации Prism
        /// </summary>
        public INavigationService NavigationService { get; }

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
        public async Task NavigateTo(TParameter parameter) =>
             await new NavigationParameters
             {{ NavigationParameterName, parameter }}.
             MapAsync(navigationParameters => NavigationService.NavigateAsync(PageName, navigationParameters));
    }
}