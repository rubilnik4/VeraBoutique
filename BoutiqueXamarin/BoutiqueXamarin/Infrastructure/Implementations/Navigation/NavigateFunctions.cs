using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Функции навигации
    /// </summary>
    public static class NavigateFunctions
    {
        /// <summary>
        /// Получить параметры навигации
        /// </summary>
        public static NavigationParameters ToNavigationParameters<TOption>(TOption options)
            where TOption : BaseNavigationOptions =>
            new NavigationParameters { { GetOptionsName<TOption>(), options } };

        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        public static string GetOptionsName<TOption>()
            where TOption : BaseNavigationOptions =>
            typeof(TOption).Name;

        /// <summary>
        /// Имя страницы для навигации
        /// </summary>
        public static string GetPageName<TPage>()
            where TPage : Page =>
            typeof(TPage).Name;
    }
}