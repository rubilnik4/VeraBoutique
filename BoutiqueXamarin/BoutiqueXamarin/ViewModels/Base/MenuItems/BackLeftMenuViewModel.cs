using System;
using System.Reactive;
using System.Threading.Tasks;
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
        public BackLeftMenuViewModel(Func<Task<INavigationResult>> navigateBackFunc)
        {
            BackNavigateCommand = ReactiveCommand.CreateFromTask(_ => navigateBackFunc());
        }

        /// <summary>
        /// Команда. Вернуться назад
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> BackNavigateCommand { get; }
    }
}