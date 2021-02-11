using System.Collections.ObjectModel;
using System.Linq;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Models.Interfaces;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService, IBoutiqueProject boutiqueProject,
                               IXamarinConfigurationDomain xamarinConfigurationDomain)
          : base(navigationService)
        {
            ChoiceViewModelItems = new ObservableCollection<ChoiceViewModelItem>(boutiqueProject.Genders.Select(gender => new ChoiceViewModelItem(gender)));
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Выбор категории";

        /// <summary>
        /// Типы пола
        /// </summary>
        public ObservableCollection<ChoiceViewModelItem> ChoiceViewModelItems { get; }
    }
}