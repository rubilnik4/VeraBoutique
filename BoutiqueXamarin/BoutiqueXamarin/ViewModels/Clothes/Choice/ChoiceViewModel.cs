using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choice.ChoiceViewModelItems;
using BoutiqueXamarinCommon.Models.Interfaces;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;
using RestSharp;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService, IBoutiqueXamarinProject boutiqueXamarinProject)
          : base(navigationService)
        {
            ChoiceGenderViewModelItems = GetChoiceGenderItems(boutiqueXamarinProject);
        }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceGenderViewModelItem> ChoiceGenderViewModelItems { get; }

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceGenderViewModelItem> GetChoiceGenderItems(IBoutiqueXamarinProject boutiqueXamarinProject) =>
            boutiqueXamarinProject.GenderCategories.
            Select(genderCategory => new ChoiceGenderViewModelItem(genderCategory)).
            ToList();
    }
}