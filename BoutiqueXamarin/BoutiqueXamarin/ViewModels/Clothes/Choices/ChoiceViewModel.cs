using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels;
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
    public class ChoiceViewModel : NavigationBaseViewModel<ChoiceNavigationParameters, IChoiceNavigationService>
    {
        public ChoiceViewModel(IGenderRestService genderRestService, IChoiceNavigationService choiceNavigationService,
                               IClothesNavigationService clothesNavigationService)
            : base(choiceNavigationService)
        {
            _choiceGenderViewModelItems = Observable.FromAsync(() => GetChoiceGenderItems(clothesNavigationService, genderRestService)).
                                          ToProperty(this, nameof(ChoiceGenderViewModelItems), scheduler: RxApp.MainThreadScheduler);
            //this.WhenAnyValue(x => x.ChoiceGenderViewModelItems).
            //     Where(choiceItems => choiceItems != null).
            //     Select(choiceItems => choiceItems.First()).
            //     Subscribe(choiceItem => SelectedChoiceViewModelItem = choiceItem);
        }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ChoiceGenderViewModelItem>> _choiceGenderViewModelItems;

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceGenderViewModelItem> ChoiceGenderViewModelItems =>
            _choiceGenderViewModelItems.Value;

        ///// <summary>
        ///// Выбранная страница
        ///// </summary>
        //private ChoiceGenderViewModelItem _selectedChoiceViewModelItem;

        ///// <summary>
        ///// Выбранная страница
        ///// </summary>
        //public ChoiceGenderViewModelItem SelectedChoiceViewModelItem
        //{
        //    get => _selectedChoiceViewModelItem;
        //    set => this.RaiseAndSetIfChanged(ref _selectedChoiceViewModelItem, value);
        //}

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static async Task<IReadOnlyCollection<ChoiceGenderViewModelItem>> GetChoiceGenderItems(IClothesNavigationService clothesNavigationService,
                                                                                                       IGenderRestService genderRestService) =>
            await genderRestService.GetGenderCategories().
            ResultCollectionOkTaskAsync(genderCategories =>
                genderCategories.Select(genderCategory => new ChoiceGenderViewModelItem(clothesNavigationService, genderCategory))).
            MapTaskAsync(result => result.Value.ToList());

    }
}