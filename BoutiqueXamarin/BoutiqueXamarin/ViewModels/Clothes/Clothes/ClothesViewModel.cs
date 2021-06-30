using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Prism.Common;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel : NavigationBaseViewModel<ClothesNavigationParameters>
    {
        public ClothesViewModel(IClothesRestService clothesRestService, IClothesDetailNavigationService clothesDetailNavigationService)
        {
            ClothesCommand = ReactiveCommand.CreateFromTask<ClothesNavigationParameters, IReadOnlyCollection<ClothesColumnViewModelItem>>(
                parameters => GetClothesItems(parameters, clothesRestService, clothesDetailNavigationService));
            _clothesViewModelColumnItems = ClothesCommand.
                                           ToProperty(this, nameof(ClothesViewModelColumnItems), scheduler: RxApp.MainThreadScheduler);

            this.WhenAnyValue(x => x.NavigationParameters).
                 Where(clothesParameters => clothesParameters != null).
                 InvokeCommand(this, x => x.ClothesCommand!);
        }

        public ReactiveCommand<ClothesNavigationParameters, IReadOnlyCollection<ClothesColumnViewModelItem>> ClothesCommand { get; }

        /// <summary>
        /// Одежда
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ClothesColumnViewModelItem>> _clothesViewModelColumnItems;

        /// <summary>
        /// Одежда
        /// </summary>
        public IReadOnlyCollection<ClothesColumnViewModelItem> ClothesViewModelColumnItems =>
            _clothesViewModelColumnItems.Value;

        ///// <summary>
        ///// Размеры для фильтрации
        ///// </summary>
        //public IReadOnlyCollection<string> FilterSizes =>
        //    ClothesViewModelColumnItems.
        //    SelectMany(clothesColumn => clothesColumn.ClothesViewModelItems).
        //    Select(clothesItem => clothesItem.ClothesDomain.)

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        public static async Task<IReadOnlyCollection<ClothesColumnViewModelItem>> GetClothesItems(ClothesNavigationParameters clothesParameters,
                                                                                                  IClothesRestService clothesRestService,
                                                                                                  IClothesDetailNavigationService clothesDetailNavigationService) =>
            await clothesRestService.GetClothes(clothesParameters.GenderType, clothesParameters.ClothesType).
            ResultCollectionOkTaskAsync(clothesDomains => GetClothesItemsFromDomains(clothesDomains, clothesRestService, clothesDetailNavigationService)).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   result => new List<ClothesColumnViewModelItem>());

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        private static IReadOnlyCollection<ClothesColumnViewModelItem> GetClothesItemsFromDomains(IEnumerable<IClothesDomain> clothesDomains,
                                                                                                 IClothesRestService clothesRestService,
                                                                                                 IClothesDetailNavigationService clothesDetailNavigationService) =>
            clothesDomains.
            Select(clothesDomain => new ClothesViewModelItem(clothesDomain, clothesRestService, clothesDetailNavigationService)).
            Map(tt => tt).
            ToList().
            Map(clothesItems => (clothesItems.Where((clothes, index) => index % 2 == 0),
                                 clothesItems.Where((clothes, index) => index % 2 != 0))).
            Map(clothesPair => clothesPair.Item1.ZipLong(clothesPair.Item2,
                                                         (first, second) => new ClothesColumnViewModelItem(first, second))).
            ToList();
    }
}