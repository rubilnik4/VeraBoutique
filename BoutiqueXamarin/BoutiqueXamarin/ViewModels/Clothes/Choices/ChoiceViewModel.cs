using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : NavigationBaseViewModel<ChoiceNavigationParameters>
    {
        public ChoiceViewModel(IClothesNavigationService clothesNavigationService, IGenderRestService genderRestService)
        {
            _clothesNavigationService = clothesNavigationService;
            _genderRestService = genderRestService;
        }

        /// <summary>
        /// Сервис навигации к странице одежды
        /// </summary>
        private readonly IClothesNavigationService _clothesNavigationService;

        /// <summary>
        /// Сервис типа пола
        /// </summary>
        private readonly IGenderRestService _genderRestService;

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        private IReadOnlyCollection<ChoiceGenderBaseViewModelItem>? _choiceGenderViewModelItems;

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceGenderBaseViewModelItem>? ChoiceGenderViewModelItems
        {
            get => _choiceGenderViewModelItems;
            private set => SetProperty(ref _choiceGenderViewModelItems, value);
        }

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override async Task<IResultError> InitializeAction(ChoiceNavigationParameters choiceNavigationParameters) =>
            await GetChoiceGenderItems(_clothesNavigationService, _genderRestService).
            ResultCollectionVoidOkTaskAsync(choiceGenderItems => ChoiceGenderViewModelItems = choiceGenderItems);

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static async Task<IResultCollection<ChoiceGenderBaseViewModelItem>> GetChoiceGenderItems(IClothesNavigationService clothesNavigationService,
                                                                                                         IGenderRestService genderRestService) =>
            await genderRestService.GetGenderCategories().
            ResultCollectionOkTaskAsync(genderCategories =>
                genderCategories.Select(genderCategory => new ChoiceGenderBaseViewModelItem(clothesNavigationService, genderCategory)));

    }
}