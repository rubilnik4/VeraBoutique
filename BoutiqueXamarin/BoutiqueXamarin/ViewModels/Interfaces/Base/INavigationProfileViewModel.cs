using BoutiqueXamarin.ViewModels.Base.MenuItems;

namespace BoutiqueXamarin.ViewModels.Interfaces.Base
{
    /// <summary>
    /// Модель навигации к личным данным
    /// </summary>
    public interface INavigationProfileViewModel
    {
        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        UserRightMenuViewModel UserRightMenuViewModel { get; }
    }
}