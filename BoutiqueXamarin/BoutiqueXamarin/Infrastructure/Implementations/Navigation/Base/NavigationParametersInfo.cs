using System;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base
{
    /// <summary>
    /// Информация о параметрах навигации
    /// </summary>
    public static class NavigationParametersInfo
    {
        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        public static string GetNavigationParameterName<TParameter>()
            where TParameter : BaseNavigationParameters =>
            typeof(TParameter).Name;
    }
}