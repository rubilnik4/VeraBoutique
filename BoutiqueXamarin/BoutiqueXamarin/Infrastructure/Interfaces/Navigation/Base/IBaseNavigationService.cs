﻿using System.Threading.Tasks;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base
{
    /// <summary>
    /// Базовый сервис навигации
    /// </summary>
    public interface IBaseNavigationService<in TParameter>
        where TParameter : EmptyNavigationParameters
    {
        /// <summary>
        /// Сервис навигации Prism
        /// </summary>
        INavigationService NavigationService { get; }

        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        string NavigationParameterName { get; }

        /// <summary>
        /// Имя страницы для навигации
        /// </summary>
        string PageName { get; }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task NavigateTo(TParameter parameter);
    }
}