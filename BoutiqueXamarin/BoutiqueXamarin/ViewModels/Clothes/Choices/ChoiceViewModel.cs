using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarinCommon.Models.Interfaces;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService, IBoutiqueXamarinProject boutiqueXamarinProject)
          : base(navigationService)
        {
            ChoiceGenderViewModelItems = GetChoiceGenderItems(navigationService, boutiqueXamarinProject);
        }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceGenderViewModelItem> ChoiceGenderViewModelItems { get; }

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceGenderViewModelItem> GetChoiceGenderItems(INavigationService navigationService, 
                                                                                           IBoutiqueXamarinProject boutiqueXamarinProject) =>
            boutiqueXamarinProject.GenderCategories.
            Select(genderCategory => new ChoiceGenderViewModelItem(navigationService, genderCategory)).
            ToList();
    }
}