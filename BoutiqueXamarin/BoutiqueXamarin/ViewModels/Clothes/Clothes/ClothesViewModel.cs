﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
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
            _clothesTypeDomain = this.WhenAnyValue(x => x.NavigationParameters).
                                      Where(clothesParameters => clothesParameters != null).
                                      Select(clothesParameters => clothesParameters!.ClothesTypeDomain).
                                      ToProperty(this, nameof(ClothesTypeDomain));

            _clothes = this.WhenAnyValue(x => x.NavigationParameters).
                            Where(parameters => parameters != null).
                            SelectMany(parameters => Observable.FromAsync(() => GetClothes(parameters!, clothesRestService))).
                            ToProperty(this, nameof(Clothes), scheduler: RxApp.MainThreadScheduler);

            _clothesViewModelColumnItems = this.WhenAnyValue(x => x.Clothes).
                                                Where(clothes => clothes!= null).
                                                Select(clothes => GetClothesItems(clothes, clothesRestService, clothesDetailNavigationService)).
                                                ToProperty(this, nameof(ClothesViewModelColumnItems));

            _filterSizeViewModelItems = this.WhenAnyValue(x => x.Clothes, x => x.NavigationParameters, 
                                             (clothes, parameters) => (clothes, parameters)).
                                Where(clothesNavigate => clothesNavigate.clothes != null && clothesNavigate.parameters != null).
                                Select(clothesNavigate => GetFilterSizes(clothesNavigate.clothes, 
                                                                         clothesNavigate.parameters!.ClothesTypeDomain.SizeTypeDefault)).
                                ToProperty(this, nameof(FilterSizeViewModelItems));
        }

        /// <summary>
        /// Одежда
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<IClothesDetailDomain>> _clothes;

        /// <summary>
        /// Одежда
        /// </summary>
        private IReadOnlyCollection<IClothesDetailDomain> Clothes =>
            _clothes.Value;

        /// <summary>
        /// Одежда. Модели
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ClothesColumnViewModelItem>> _clothesViewModelColumnItems;

        /// <summary>
        /// Одежда. Модели
        /// </summary>
        public IReadOnlyCollection<ClothesColumnViewModelItem> ClothesViewModelColumnItems =>
            _clothesViewModelColumnItems.Value;

        /// <summary>
        /// Тип одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IClothesTypeDomain> _clothesTypeDomain;

        /// <summary>
        /// Тип одежды
        /// </summary>
        private IClothesTypeDomain ClothesTypeDomain =>
            _clothesTypeDomain.Value;

        /// <summary>
        /// Размеры для фильтрации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<FilterSizeViewModelItem>> _filterSizeViewModelItems;

        /// <summary>
        /// Размеры для фильтрации
        /// </summary>
        public IReadOnlyCollection<FilterSizeViewModelItem> FilterSizeViewModelItems =>
            _filterSizeViewModelItems.Value;

        /// <summary>
        /// Получить модели одежды
        /// </summary>
        private static async Task<IReadOnlyCollection<IClothesDetailDomain>> GetClothes(ClothesNavigationParameters clothesParameters,
                                                                                        IClothesRestService clothesRestService) =>
            await clothesRestService.GetClothesDetails(clothesParameters.GenderType, clothesParameters.ClothesTypeDomain.Name).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   result => new List<IClothesDetailDomain>());

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        private static IReadOnlyCollection<ClothesColumnViewModelItem> GetClothesItems(IEnumerable<IClothesDetailDomain> clothesDomains,
                                                                                       IClothesRestService clothesRestService,
                                                                                       IClothesDetailNavigationService clothesDetailNavigationService) =>
            clothesDomains.
            Select(clothesDomain => new ClothesViewModelItem(clothesDomain, clothesRestService, clothesDetailNavigationService)).
            Map(tt => tt).
            ToList().
            Map(clothesItems => (columnLeft: clothesItems.Where((clothes, index) => index % 2 == 0),
                                 columnRight: clothesItems.Where((clothes, index) => index % 2 != 0))).
            Map(clothesPair => clothesPair.columnLeft.ZipLong(clothesPair.columnRight,
                                                              (first, second) => new ClothesColumnViewModelItem(first, second))).
            ToList();

        /// <summary>
        /// Получить размеры для фильтрации
        /// </summary>
        private static IReadOnlyCollection<FilterSizeViewModelItem> GetFilterSizes(IEnumerable<IClothesDetailDomain> clothes,
                                                                                   SizeType sizeTypeDefault) =>
            clothes.
            SelectMany(clothesItem => clothesItem.SizeGroups).
            Distinct().
            OrderBy(sizeGroup => sizeGroup.SizeNormalize).
            Select(sizeGroup => sizeGroup.Sizes.FirstOrDefault(size => size.SizeType == sizeTypeDefault)).
            Where(size => size != null).
            Distinct().
            Select(size => new FilterSizeViewModelItem(size!)).
            ToList();
    }
}