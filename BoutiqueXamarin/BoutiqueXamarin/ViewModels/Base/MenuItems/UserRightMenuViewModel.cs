using System.Reactive;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base.MenuItems
{
    /// <summary>
    /// Правое меню пользователя
    /// </summary>
    public class UserRightMenuViewModel : BaseViewModel
    {
        public UserRightMenuViewModel()
        {
            UserCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default);
            CartCommand = ReactiveCommand.Create<Unit, Unit>(_ => Unit.Default);
        }

        /// <summary>
        /// Команда информации о пользователе
        /// </summary>
        public ReactiveCommand<Unit, Unit> UserCommand { get; }

        /// <summary>
        /// Команда информации о корзине
        /// </summary>
        public ReactiveCommand<Unit, Unit> CartCommand { get; }
    }
}