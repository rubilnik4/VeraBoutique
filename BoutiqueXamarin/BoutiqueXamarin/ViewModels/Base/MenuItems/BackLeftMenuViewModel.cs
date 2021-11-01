using System.Reactive;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base.MenuItems
{
    /// <summary>
    /// Меню навигации назад
    /// </summary>
    public class BackLeftMenuViewModel : BaseViewModel
    {
        public BackLeftMenuViewModel(IBackNavigationService backNavigationService)
        {
            BackNavigateCommand = ReactiveCommand.CreateFromTask(_ => backNavigationService.NavigateBack());
        }

        /// <summary>
        /// Команда. Вернуться назад
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> BackNavigateCommand { get; }
    }
}