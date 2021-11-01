using System.Reactive;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base.MenuItems
{
    /// <summary>
    /// Правое меню пользователя
    /// </summary>
    public class UserRightMenuViewModel : BaseViewModel
    {
        public UserRightMenuViewModel(INavigationServiceFactory navigationServiceFactory)
        {
            UserNavigateCommand = ReactiveCommand.CreateFromTask(_ => navigationServiceFactory.ToProfilePage());
            CartCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default);
        }

        /// <summary>
        /// Команда информации о пользователе
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> UserNavigateCommand { get; }

        /// <summary>
        /// Команда информации о корзине
        /// </summary>
        public ReactiveCommand<Unit, Unit> CartCommand { get; }
    }
}