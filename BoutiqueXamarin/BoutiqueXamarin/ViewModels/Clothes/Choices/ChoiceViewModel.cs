using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
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
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;
using ReactiveUI;
using Splat;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : NavigationBaseViewModel<EmptyNavigationParameters>
    {
        public ChoiceViewModel(IClothesNavigationService clothesNavigationService, IGenderRestService genderRestService)
        {
            _choiceGenderViewModelItems = Observable.FromAsync(() => GetChoiceGenderItems(clothesNavigationService, genderRestService)).
                                          ToProperty(this, nameof(ChoiceGenderViewModelItems), scheduler: RxApp.MainThreadScheduler);
        }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<List<ChoiceGenderBaseViewModelItem>> _choiceGenderViewModelItems;

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public List<ChoiceGenderBaseViewModelItem> ChoiceGenderViewModelItems =>
            _choiceGenderViewModelItems.Value;

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static async Task<List<ChoiceGenderBaseViewModelItem>> GetChoiceGenderItems(IClothesNavigationService clothesNavigationService,
                                                                                            IGenderRestService genderRestService) =>
            await genderRestService.GetGenderCategories().
            ResultCollectionOkTaskAsync(genderCategories =>
                genderCategories.Select(genderCategory => new ChoiceGenderBaseViewModelItem(clothesNavigationService, genderCategory))).
            MapTaskAsync(result => result.Value.ToList());

    }
}