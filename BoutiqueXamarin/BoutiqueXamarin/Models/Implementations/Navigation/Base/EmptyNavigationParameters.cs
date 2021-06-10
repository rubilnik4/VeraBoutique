using Prism.Navigation;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Base
{
    /// <summary>
    /// Пустые параметры навигации
    /// </summary>
    public class EmptyNavigationParameters : BaseNavigationParameters
    {
        /// <summary>
        /// Пустые параметры навигации
        /// </summary>
        public static NavigationParameters GetEmptyNavigationParameters =>
            new NavigationParameters { { nameof(EmptyNavigationParameters), new EmptyNavigationParameters() } };
    }
}