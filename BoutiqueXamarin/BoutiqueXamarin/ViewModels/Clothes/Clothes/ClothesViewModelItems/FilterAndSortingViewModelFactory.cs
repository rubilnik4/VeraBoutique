﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Enums;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems;
using ResultFunctional.FunctionalExtensions.Sync;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модели фильтрации
    /// </summary>
    public static class FilterAndSortingViewModelFactory
    {
        /// <summary>
        /// Получить размеры для фильтрации
        /// </summary>
        public static IReadOnlyCollection<FilterSizeViewModelItem> GetFilterSizes(IEnumerable<ClothesViewModelItem> clothes,
                                                                                  SizeType sizeTypeDefault,
                                                                                  ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand) =>
            clothes.
            SelectMany(clothesItem => clothesItem.ClothesDetail.SizeGroups).
            Distinct().
            Where(sizeGroup => sizeGroup.Sizes.Any(size => size.SizeType == sizeTypeDefault)).
            OrderBy(sizeGroup => sizeGroup.SizeNormalize).
            Select(sizeGroup => new FilterSizeViewModelItem(sizeGroup, sizeTypeDefault, clothesFilterCommand)).
            ToList();

        /// <summary>
        /// Получить цвета для фильтрации
        /// </summary>
        public static IReadOnlyCollection<FilterColorViewModelItem> GetFilterColors(IEnumerable<ClothesViewModelItem> clothes,
                                                                                    ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand) =>
            clothes.
            SelectMany(clothesItem => clothesItem.ClothesDetail.Colors).
            Distinct().
            OrderBy(color => color.Name).
            Select(color => new FilterColorViewModelItem(color, clothesFilterCommand)).
            ToList();

        /// <summary>
        /// Получить цены для фильтрации
        /// </summary>
        public static FilterPriceViewModelItem GetFilterPrices(IEnumerable<ClothesViewModelItem> clothes,
                                                               ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand) =>
            clothes.
            Select(clothesItem => clothesItem.ClothesDetail.Price).
            OrderBy(price => price).
            ToList().
            Map(prices => new FilterPriceViewModelItem(prices.First(), prices.Last(), clothesFilterCommand));

        /// <summary>
        /// Получить отфильтрованную одежду
        /// </summary>
        public static IReadOnlyList<ClothesViewModelItem> GetClothesFiltered(IEnumerable<ClothesViewModelItem> clothesItems,
                                                                             ClothesFilterViewModelCollection clothesFilter,
                                                                             ClothesSortingType clothesSortingType) =>
            GetClothesFilterChecked(clothesItems,
                                    clothesFilter.SizeItems.Where(sizeItem => sizeItem.IsChecked).
                                                  Select(sizeItem => sizeItem.SizeGroup).
                                                  ToList(),
                                    clothesFilter.ColorItems.Where(colorItem => colorItem.IsChecked).
                                                  Select(colorItem => colorItem.Color).
                                                  ToList(),
                                    (clothesFilter.PriceItem.PriceMinimumValue, clothesFilter.PriceItem.PriceMaximumValue),
                                    clothesSortingType);

        /// <summary>
        /// Получить отфильтрованную одежду с отмеченными фильтрами
        /// </summary>
        private static IReadOnlyList<ClothesViewModelItem> GetClothesFilterChecked(IEnumerable<ClothesViewModelItem> clothesItems,
                                                                                   IReadOnlyCollection<ISizeGroupMainDomain> filterSizes,
                                                                                   IReadOnlyCollection<IColorDomain> filterColors,
                                                                                   (double, double) priceRange,
                                                                                   ClothesSortingType clothesSortingType) =>
            clothesItems.
            Where(clothes => SizeFilterFunc(clothes.ClothesDetail, filterSizes) &&
                             ColorFilterFunc(clothes.ClothesDetail, filterColors) &&
                             PriceFilterFunc(clothes.ClothesDetail, priceRange)).
            Map(clothes => ClothesSorting(clothes, clothesSortingType)).
            ToList();

        /// <summary>
        /// Функция фильтрации по цвету одежды
        /// </summary>
        private static Func<IClothesDetailDomain, IReadOnlyCollection<ISizeGroupMainDomain>, bool> SizeFilterFunc =>
            (clothes, filterSizes) => filterSizes.Count == 0 ||
                                      filterSizes.All(filterSize => clothes.SizeGroups.Contains(filterSize));

        /// <summary>
        /// Функция фильтрации по цвету одежды
        /// </summary>
        private static Func<IClothesDetailDomain, IReadOnlyCollection<IColorDomain>, bool> ColorFilterFunc =>
            (clothes, filterColors) => filterColors.Count == 0 ||
                                       filterColors.All(filterColor => clothes.Colors.Contains(filterColor));

        /// <summary>
        /// Функция фильтрации по цене одежды
        /// </summary>
        private static Func<IClothesDetailDomain, (double PriceMinimum, double PriceMaximum), bool> PriceFilterFunc =>
            (clothes, priceRange) => (double)clothes.Price >= priceRange.PriceMinimum &&
                                     (double)clothes.Price <= priceRange.PriceMaximum;


        /// <summary>
        /// Сортировать список одежды
        /// </summary>
        private static IEnumerable<ClothesViewModelItem> ClothesSorting(IEnumerable<ClothesViewModelItem> clothes,
                                                                        ClothesSortingType clothesSortingType) =>
            clothesSortingType switch
            {
                ClothesSortingType.Naming => clothes.OrderBy(clothesItem => clothesItem.ClothesDetail.Name),
                ClothesSortingType.Price => clothes.OrderBy(clothesItem => clothesItem.ClothesDetail.Price),
                _ => clothes.OrderBy(clothesItem => clothesItem.ClothesDetail.Name),
            };
    }
}