using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.ViewModels.Base;
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
            ChoiceViewModelItems = GetChoiceViewModelItems(boutiqueXamarinProject);
        }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<ChoiceViewModelItem> ChoiceViewModelItems { get; }

        /// <summary>
        /// Получить модели типа одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceViewModelItem> GetChoiceViewModelItems(IBoutiqueXamarinProject boutiqueXamarinProject) =>
            boutiqueXamarinProject.Genders.
            Select(gender => new ChoiceViewModelItem(gender)).
            ToList();
    }
}