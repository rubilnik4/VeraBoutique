using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarinCommon.Models.Interfaces;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : BaseViewModel
    {
        public ChoiceViewModel(IClothesNavigationService clothesNavigationService, IBoutiqueXamarinProject boutiqueXamarinProject)
        {
            ChoiceGenderViewModelItems = GetChoiceGenderItems(clothesNavigationService, boutiqueXamarinProject);
        }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceGenderBaseViewModelItem> ChoiceGenderViewModelItems { get; }

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceGenderBaseViewModelItem> GetChoiceGenderItems(IClothesNavigationService clothesNavigationService, 
                                                                                               IBoutiqueXamarinProject boutiqueXamarinProject) =>
            boutiqueXamarinProject.GenderCategories.
            Select(genderCategory => new ChoiceGenderBaseViewModelItem(clothesNavigationService, genderCategory)).
            ToList();
    }
}