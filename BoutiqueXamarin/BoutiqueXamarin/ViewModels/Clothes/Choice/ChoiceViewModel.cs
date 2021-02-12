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
        public ChoiceViewModel(INavigationService navigationService, IGenderRestService genderRestService)
          : base(navigationService)
        {
            _genderRestService = genderRestService;
        }

        /// <summary>
        /// Сервис типа пола
        /// </summary>
        private readonly IGenderRestService _genderRestService;

        /// <summary>
        /// Типы пола
        /// </summary>
        public ObservableCollection<ChoiceViewModelItem> ChoiceViewModelItems { get; private set; } =
            new ObservableCollection<ChoiceViewModelItem>();

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override async Task<IResultError> InitializeAction() =>
            await _genderRestService.Get().
            ResultCollectionOkTaskAsync(genders => genders.Select(gender => new ChoiceViewModelItem(gender))).
            ResultCollectionVoidOkTaskAsync(
                choiceViewModelItems =>
                {
                    ChoiceViewModelItems.Add(choiceViewModelItems.First());
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ChoiceViewModelItems)));
                });
    }
}