using System.Reactive;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base.MenuItems
{
    /// <summary>
    /// Меню навигации назад
    /// </summary>
    public class BackLeftMenuViewModel : BaseViewModel
    {
        public BackLeftMenuViewModel(IBackNavigationService navigateService)
        {
            BackNavigateCommand = ReactiveCommand.CreateFromTask(_ => navigateService.NavigateBack());
        }

        /// <summary>
        /// Команда. Вернуться назад
        /// </summary>
        public ReactiveCommand<Unit, Unit> BackNavigateCommand { get; }
    }
}