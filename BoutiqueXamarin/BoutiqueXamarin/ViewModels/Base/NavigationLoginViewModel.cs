﻿using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией и страницей авторизации
    /// </summary>
    public abstract class NavigationLoginViewModel<TParameter, TNavigate> : NavigationBaseViewModel<TParameter, TNavigate>
        where TParameter : BaseNavigationParameters
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationLoginViewModel(TNavigate navigateService, ILoginNavigationService loginNavigationService)
            : base(navigateService)
        {
            UserRightMenuViewModel = new UserRightMenuViewModel(loginNavigationService);
        }

        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        public UserRightMenuViewModel UserRightMenuViewModel { get; }
    }
}