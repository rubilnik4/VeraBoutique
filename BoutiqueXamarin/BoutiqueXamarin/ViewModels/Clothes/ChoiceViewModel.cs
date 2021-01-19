using BoutiqueXamarin.ViewModels.Base;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService)
          : base(navigationService)
        {
           
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Выбор категории";
    }
}