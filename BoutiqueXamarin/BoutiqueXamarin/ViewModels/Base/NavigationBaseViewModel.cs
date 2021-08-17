﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using BoutiqueXamarinCommon.Models.Enums.ViewModels;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с навигацией
    /// </summary>
    public abstract class NavigationBaseViewModel<TParameter, TNavigate> : ErrorBaseViewModel
        where TParameter : EmptyNavigationParameters
        where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationBaseViewModel(TNavigate navigateService)
        {
            NavigateService = navigateService;
            BackLeftMenuViewModel = new BackLeftMenuViewModel<TParameter, TNavigate>(navigateService);
            UserRightMenuViewModel = new UserRightMenuViewModel();
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        public TNavigate NavigateService { get; }

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        public BackLeftMenuViewModel<TParameter, TNavigate> BackLeftMenuViewModel { get; }

        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        public UserRightMenuViewModel UserRightMenuViewModel { get; }

        /// <summary>
        /// Параметры навигации
        /// </summary>
        private TParameter? _navigationParameters;

        /// <summary>
        /// Параметры навигации
        /// </summary>
        protected TParameter? NavigationParameters
        {
            get => _navigationParameters;
            set => this.RaiseAndSetIfChanged(ref _navigationParameters, value);
        }

        /// <summary>
        /// Параметры инициализации формы с изменением состояния
        /// </summary>
        public override void Initialize(INavigationParameters parameters) =>
            NavigationParameters = GetNavigationParameters(parameters);

        /// <summary>
        /// Преобразовать параметры навигации
        /// </summary>
        private static TParameter GetNavigationParameters(INavigationParameters parameters) =>
            parameters.GetValue<TParameter>(NavigationParametersInfo.GetNavigationParameterName<TParameter>());
    }
}