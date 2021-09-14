using System.Reactive;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base.MenuItems
{
    /// <summary>
    /// Правое меню пользователя
    /// </summary>
    public class UserRightMenuViewModel : BaseViewModel
    {
        public UserRightMenuViewModel(ILoginNavigationService loginNavigationService)
        {
            UserNavigateCommand = ReactiveCommand.CreateFromTask(_ => loginNavigationService.NavigateTo());
            CartCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default);
        }

        /// <summary>
        /// Команда информации о пользователе
        /// </summary>
        public ReactiveCommand<Unit, Unit> UserNavigateCommand { get; }

        /// <summary>
        /// Команда информации о корзине
        /// </summary>
        public ReactiveCommand<Unit, Unit> CartCommand { get; }
    }
}