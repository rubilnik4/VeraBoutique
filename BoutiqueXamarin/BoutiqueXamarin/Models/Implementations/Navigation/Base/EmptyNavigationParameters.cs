using Prism.Navigation;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Base
{
    /// <summary>
    /// Базовые параметры навигации
    /// </summary>
    public class EmptyNavigationParameters
    {
        /// <summary>
        /// Являются ли параметры пустыми
        /// </summary>
        public bool IsEmpty => 
            GetType() == typeof(EmptyNavigationParameters);

        /// <summary>
        /// Пустые параметры навигации
        /// </summary>
        public static NavigationParameters GetEmptyNavigationParameters =>
            new NavigationParameters { { nameof(EmptyNavigationParameters), new EmptyNavigationParameters() } };
    }
}