using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией и страницей авторизации
    /// </summary>
    public abstract class NavigationProfileViewModel<TParameter, TNavigate> : NavigationBaseViewModel<TParameter, TNavigate>
        where TParameter : BaseNavigationOptions
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationProfileViewModel(TNavigate navigateService, IProfileNavigationService profileNavigationService, 
                                           IErrorNavigationService errorNavigationService)
            : base(navigateService, errorNavigationService)
        {
            UserRightMenuViewModel = new UserRightMenuViewModel(profileNavigationService);
        }

        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        public UserRightMenuViewModel UserRightMenuViewModel { get; }
    }
}